using UnityEngine;

namespace MusaUtils.UpgradeSystem
{
    [CreateAssetMenu(fileName = "NewWallet", menuName = "UpgradeSystem/Wallet")]
    public class WalletData : ScriptableObject
    {
        public int currency;
    }
}
