using System;
using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.UpgradeSystem
{
    [CreateAssetMenu(fileName = "NewUpgradeableItem", menuName = "UpgradeSystem/Upgradeable Item")]
    public class ItemData : ScriptableObject
    {
        public int index;
        public string itemName;
        public int currentLevel;

        public List<UpgradeValueLevels> levels;
    }

    [Serializable]
    public class UpgradeValueLevels
    {
        public int upgradeableValue;
        public int upgradePrice;
    }
}
