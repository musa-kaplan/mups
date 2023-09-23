using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.General
{
    public class DataContainer : MonoBehaviour
    {
        public static DataContainer dataContainer;
        public WalletManager walletManager;

        private void Awake()
        {
            while (dataContainer == null) {dataContainer = this;}

            Application.targetFrameRate = 150;
            QualitySettings.vSyncCount = 0;
        }
    }
}