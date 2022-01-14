using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Killable
{
    public GameObject _upgradeGO;
    public UpgradeData _upgrade;

    public void Upgrade(UpgradeData upgrade)
    {
        if (ResourcesManager._current.MoveResources(-upgrade._ironPrice, -upgrade._goldPrice))
        {
            if (_upgradeGO)
            {
                Destroy(_upgradeGO);
            }
            _upgradeGO = Instantiate(upgrade._prefab, transform);
            _upgrade = upgrade;
        }
    }
}
