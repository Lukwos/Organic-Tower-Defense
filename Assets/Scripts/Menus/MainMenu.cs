using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UIDocument _document;

    public GameObject _turretPrefab;
    public GameObject _excavatorPrefab;

    public bool _mouseOnUI;

    Label _moneyLabel;
    Button _nextWaveButton;

    Button _turretButton;
    Button _excavatorButton;

    void OnEnable()
    {
        _moneyLabel = _document.rootVisualElement.Q<Label>("money-label");

        _nextWaveButton = _document.rootVisualElement.Q<Button>("next-wave-button");
        _nextWaveButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(MainManager._current.StartWaveCoroutine()));

        _turretButton = _document.rootVisualElement.Q<Button>("turret-button");
        _turretButton.RegisterCallback<ClickEvent>(evt => MainManager._current.SelectUnit(_turretPrefab));

        _excavatorButton = _document.rootVisualElement.Q<Button>("excavator-button");
        _excavatorButton.RegisterCallback<ClickEvent>(evt => MainManager._current.SelectUnit(_excavatorPrefab));

        _document.rootVisualElement.RegisterCallback<MouseEnterEvent>(evt => _mouseOnUI = true);
        _document.rootVisualElement.RegisterCallback<MouseLeaveEvent>(evt => _mouseOnUI = false);
    }

    public void UpdateUI(int iron, int gold) { _moneyLabel.text = $"Iron : {iron} | Gold : {gold}"; }
}
