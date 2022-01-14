using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager _current;

    public int _iron;
    public int _gold;

    void Awake()
    {
        _current = this;
    }

    void Start()
    {
        MainManager._current._mainMenu.UpdateUI(_iron, _gold);
    }

    public bool MoveResources(int iron, int gold)
    {
        if (_iron + iron >= 0 && _gold + gold >= 0)
        {
            _iron += iron;
            _gold += gold;
            MainManager._current._mainMenu.UpdateUI(_iron, _gold);
            return true;
        }
        return false;
    }
}
