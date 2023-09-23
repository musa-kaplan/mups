using System.Collections.Generic;
using MusaUtils.Templates.ArcadeIdle.General;
using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.Data
{
    [CreateAssetMenu(fileName = "NewWallet", menuName = "MU/Wallet Data", order = 0)]
    public class WalletData : ScriptableObject
    {
        public List<InfoClasses.CurrencyInfo> currencies;

        public void Save()
        {
            foreach (var c in currencies)
            {
                PlayerPrefs.SetInt("Currency" + c.currencyType.ToString(), c.amount);
            }
        }

        public void Load()
        {
            foreach (var c in currencies)
            {
                if (PlayerPrefs.HasKey("Currency" + c.currencyType.ToString()))
                {
                    c.amount = PlayerPrefs.GetInt("Currency" + c.currencyType);
                }
                else
                {
                    if (c.currencyType.Equals(InfoClasses.CurrencyType.Money))
                    {
                        c.amount = c.startAmount;
                    }
                }
            }
        }
    }
}