using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class MainManager : MonoBehaviour
{
    public static MainManager _current;

    [Header("Unit")]
    public int _unitCostPrice = 100;
    public GameObject _selectedUnit;

    [Header("Spawning")]
    public List<GameObject> _enemyPrefabs;
    public List<Transform> _spawnPoints;
    public float _spawnCooldown = 1;
    int _wave = 1;

    [Header("Cursor")]
    public GameObject _cursor;
    public Range _range;
    public float _cursorValidDistance = 0;
    public bool _enableCursor = true;
    Material _cursorMaterial;

    [Header("UI")]
    public MainMenu _mainMenu;
    public UnitMenu _unitMenu;

    void Awake() { _current = this; }

    void Start()
    {
        _cursorMaterial = _cursor.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (_enableCursor && !_mainMenu._mouseOnUI)
        {
            _cursor.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0)) OnLeftClick();
            if (Input.GetMouseButtonDown(1)) OnRightClick();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Floor")))
            {
                _cursor.transform.position = hit.point + Vector3.up * 0.01f;
            }

            _cursorMaterial.color = HaveSpace() ? Color.green : Color.red;
        }
        else
        {
            _cursor.gameObject.SetActive(false);
        }

    }

    public void ActivateCursor(bool active) { _enableCursor = active; }

    public void SelectUnit(GameObject unit) { _selectedUnit = unit; }

    void OnLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var isRangeActive = false;
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Floor", "Ally")))
        {
            GameObject hitGO = hit.transform.gameObject;
            if (hitGO.layer == LayerMask.NameToLayer("Floor"))
            {
                SpawnUnit(hit.point);
                _selectedUnit = null;
            }
            else if (hitGO.layer == LayerMask.NameToLayer("Ally"))
            {
                Turret turret = hitGO.GetComponentInChildren<Turret>();
                if (turret)
                {
                    isRangeActive = true;
                    _range.Set(turret);
                }
            }
        }
        if (!isRangeActive)
        {
            _range.Set(null);
        }
    }

    

    void OnRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Ally")))
        {
            GameObject hitGO = hit.transform.gameObject;
            var unit = hitGO.GetComponentInParent<Unit>();
            if (unit)
            {
                _unitMenu.OpenMenu(unit);
            }
        }
    }

    void SpawnUnit(Vector3 position)
    {
        if (HaveSpace() && ResourcesManager._current.MoveResources(-_unitCostPrice, 0))
        {
            Instantiate(_selectedUnit, position, Quaternion.identity);
        }
    }

    bool HaveSpace()
    {
        if (!_selectedUnit) return false;
        foreach (GameObject turret in GameObject.FindGameObjectsWithTag("Ally"))
        {
            float distance = Vector3.Distance(_cursor.transform.position, turret.transform.position);
            if (distance < _cursorValidDistance)
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator StartWaveCoroutine()
    {
        Debug.Log($"Wave {_wave}");
        var enemies = new List<GameObject>();
        int spawnIndex = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[spawnIndex];

        foreach (Excavator excavator in FindObjectsOfType<Excavator>())
        {
            excavator.Excavate();
        }

        for (int i = 0; i < _wave * _wave; i++)
        {
            int enemyIndex = Random.Range(0, _enemyPrefabs.Count);
            enemies.Add(Instantiate(_enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation));
            yield return new WaitForSeconds(_spawnCooldown);
        }
        yield return new WaitUntil(() =>
        {
            enemies.RemoveAll(v => v == null);
            return enemies.Count == 0;
        });

        Debug.Log("Wave Cleared");

        _wave++;

    }
}
