using MusaUtils.Templates.ArcadeIdle.Data;
using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.General
{
    public class WalletManager : MonoBehaviour
    {
        [SerializeField] private WalletData walletData;

        public string GetStringFormatOfCurrency(InfoClasses.CurrencyType cType)
        {
            var amount = GetCurrency(cType).amount;
            return amount switch
            {
                /*if (amount > 999999) { return (amount / 1000000).ToString() + "." + (amount % 1000000).ToString("F1") + "M"; }
            return amount > 999 ? (amount / 1000f).ToString("F1") + "K" : amount.ToString();*/
                > 999999 => (amount / 1000000) + "." + (amount % 1000000) / 10000 + "M",
                > 999 => (amount / 1000) + "K",
                _ => "0"
            };
        }
        
        public string GetStringFormatByAmount(int amount)
        {
            return amount switch
            {
                > 999999 => (amount / 1000000) + "." + (amount % 1000000) / 10000 + "M",
                > 999 => (amount / 1000) + "K",
                _ => "0"
            };
        }

        public int GetCurrencyAmount(InfoClasses.CurrencyType cType)
        {
            var amount = GetCurrency(cType).amount;
            return amount;
        }
        
        private void CurrencyIncreasing(InfoClasses.CurrencyType currencyType, int amount)
        {
            GetCurrency(currencyType).amount += amount;
        }

        private void CurrencyReducing(InfoClasses.CurrencyType currencyType, int amount)
        {
            GetCurrency(currencyType).amount -= amount;
        }

        public InfoClasses.CurrencyInfo GetCurrency(InfoClasses.CurrencyType cType)
        {
            foreach (var currency in walletData.currencies)
            {
                if (currency.currencyType.Equals(cType))
                {
                    return currency;
                }
            }

            return null;
        }

        public bool IsThereAnyMoney(InfoClasses.CurrencyType cType, int amount)
        {
            return GetCurrency(cType).amount >= amount;
        }

        public void SaveCurrencies()
        {
            walletData.Save();
        }

        public void LoadCurrencies()
        {
            walletData.Load();
        }

        private void OnEnable()
        {
            LoadCurrencies();
            GameEvents.OnCurrencyIncrease += CurrencyIncreasing;
            GameEvents.OnCurrencyReduce += CurrencyReducing;
        }

        private void OnDisable()
        {
            SaveCurrencies();
            GameEvents.OnCurrencyIncrease -= CurrencyIncreasing;
            GameEvents.OnCurrencyReduce -= CurrencyReducing;
        }
    }
}
