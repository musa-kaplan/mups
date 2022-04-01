using UnityEngine;

namespace MusaUtils.UpgradeSystem
{
    public class UpgradeableItem : MonoBehaviour, IUpgradeable<ItemData>
    {
        [SerializeField] private WalletData wallet;
        
        public void OnUpgradeEvents(ItemData data)
        {
            data.currentLevel += wallet.currency >= data.levels[data.currentLevel].upgradePrice ? 1 : 0;
        }

        public void UpgradeVisuals()
        {
            //ON UPGRADE VISUALS
        }

        private void OnEnable()
        {
            UpgradeActions<ItemData>.onUpgrade += OnUpgradeEvents;
        }

        private void OnDisable()
        {
            UpgradeActions<ItemData>.onUpgrade -= OnUpgradeEvents;
        }
    }
}
