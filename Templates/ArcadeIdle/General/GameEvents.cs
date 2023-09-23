using System;
using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.General
{
    public class GameEvents : MonoBehaviour
    {
        public static event Action<InfoClasses.CurrencyType, int> OnCurrencyIncrease;
        public static void CurrencyIncrease(InfoClasses.CurrencyType cType, int amount) =>
            OnCurrencyIncrease?.Invoke(cType, amount);
        
        
        public static event Action<int> OnMoneyEarnedUi;
        public static void MoneyEarnedUi(int amount) => OnMoneyEarnedUi?.Invoke(amount);
        
        
        public static event Action<InfoClasses.CurrencyType, int> OnCurrencyReduce;
        public static void CurrencyReduce(InfoClasses.CurrencyType cType, int amount) =>
            OnCurrencyReduce?.Invoke(cType, amount);


        public static event Action<InfoClasses.UpgradeType> OnUpgraded;
        public static void Upgraded(InfoClasses.UpgradeType uType) => OnUpgraded?.Invoke(uType);
    }
}
