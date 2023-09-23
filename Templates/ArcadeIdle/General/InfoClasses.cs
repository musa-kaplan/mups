using System;
using System.Collections.Generic;
using MusaUtils.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MusaUtils.Templates.ArcadeIdle.General
{
    public abstract class InfoClasses
    {
        public enum CurrencyType{Money}
        public enum UpgradeType{CharacterSpeed, CharacterCapacity, WorkerSpeed, WorkerCapacity}
        public enum ParticleType{SmokeBurst}

        [Serializable]  
        public class CurrencyInfo
        {
            public CurrencyType currencyType;
            public MonoPools currencyPool;
            public Sprite currencySprite;
            public int amount;
            public int singularUnitAmount;
            public int startAmount;
        }
        
        [Serializable]
        public class CurrencyUiElement
        {
            public CurrencyType currencyType;
            public Image currencyImage;
            public Animator moneyAnimator;
            public TextMeshProUGUI currencyAmount;
        }
        
        [Serializable]
        public class UpgradeItemLevel
        {
            public float amount;
            public int upgradePrice;
        }
        
        [Serializable]
        public class UpgradeUiElement
        {
            public TextMeshProUGUI header;
            public Image image;
            public Button upgradeButton;
            public TextMeshProUGUI price;
            public Image currencyImage;
            public Image background;
        }
        
        [Serializable]
        public class ParticlesByType
        {
            public ParticleType particleType;
            public ParticleSystem particle;
        }
    }
}
