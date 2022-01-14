using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeData", order = 1)]
public class UpgradeData : ScriptableObject
{
    public int _ironPrice = 500;
    public int _goldPrice = 0;
    public int _maxHealth = 100;
    public GameObject _prefab;

    public List<UpgradeData> _nextUpgrades;
}
