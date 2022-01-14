using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitMenu : MonoBehaviour
{
    public UIDocument _document;
    public Unit _unit;

    Label _healthLabel;
    VisualElement _upgradesElement;
    Button _removeButton;
    Button _closeButton;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        _healthLabel = _document.rootVisualElement.Q<Label>("health-label");
        _healthLabel.text = $"Life : {_unit._health}";

        _upgradesElement = _document.rootVisualElement.Q<VisualElement>("upgrades-element");
        _upgradesElement.Clear();
        foreach (UpgradeData upgrade in _unit._upgrade._nextUpgrades)
        {
            var button = new Button();
            button.text = upgrade.name;
            button.RegisterCallback<ClickEvent>(evt =>
            {
                _unit.Upgrade(upgrade);
                UpdateUI();
            });
            _upgradesElement.Add(button);
        }

        _removeButton = _document.rootVisualElement.Q<Button>("remove-button");
        _removeButton.RegisterCallback<ClickEvent>(evt => RemoveUnit());

        _closeButton = _document.rootVisualElement.Q<Button>("close-button");
        _closeButton.RegisterCallback<ClickEvent>(evt => CloseMenu());
    }

    void RemoveUnit()
    {
        Destroy(_unit.gameObject);
        CloseMenu();
    }

    void CloseMenu()
    {
        MainManager._current.ActivateCursor(true);
        gameObject.SetActive(false);
    }

    public void OpenMenu(Unit unit)
    {
        _unit = unit;
        MainManager._current.ActivateCursor(false);
        gameObject.SetActive(true);
    }
}
