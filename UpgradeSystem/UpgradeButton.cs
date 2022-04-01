using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MusaUtils.UpgradeSystem
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private ItemData dataToUpgrade;
        private Button thisButton;

        private void Start()
        {
            thisButton = GetComponent<Button>();
            CheckIsMax(dataToUpgrade);
        }

        private async void CheckIsMax(ItemData data)
        {
            await UniTask.DelayFrame(1);
            thisButton.interactable = data.currentLevel < data.levels.Count;
        }

        public void LetsUpgrade()
        {
            UpgradeActions<ItemData>.Upgrade(dataToUpgrade);
        }

        private void OnEnable()
        {
            UpgradeActions<ItemData>.onUpgrade += CheckIsMax;
            
            thisButton.onClick.AddListener(LetsUpgrade);
        }

        private void OnDisable()
        {
            UpgradeActions<ItemData>.onUpgrade -= CheckIsMax;
        }
    }
}
