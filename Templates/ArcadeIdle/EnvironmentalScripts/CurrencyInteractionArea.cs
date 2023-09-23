using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using MusaUtils.Pooling;
using MusaUtils.Templates.ArcadeIdle.General;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MusaUtils.Templates.ArcadeIdle.EnvironmentalScripts
{
    public class CurrencyInteractionArea : MonoBehaviour
    {
        public int interactionId;
        public bool isDone;
        public int neededAmount;
        public int currAmount;
        
        [SerializeField] private InfoClasses.CurrencyType currencyType;
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI neededAmountText;
        [SerializeField] private Transform claimPoint;
        [SerializeField] private UnityEvent[] onDoneEvents;
        [SerializeField] private Image fillImage;
        
        private Transform player;
        private DataContainer dataContainer;
        private MonoPools currencyPool;
        private bool isObtaining;
        private bool doneForNow;
        private int singularUnitAmount;

        private void Start()
        {
            dataContainer = DataContainer.dataContainer;
            player = Cookies.QuickFind().transform;
            currAmount = PlayerPrefs.GetInt(transform.parent.name + interactionId);
            singularUnitAmount = dataContainer.walletManager.GetCurrency(currencyType).singularUnitAmount;

            currencyPool = dataContainer.walletManager.GetCurrency(currencyType).currencyPool;
        }

        private float stayingTimer;
        private void Update()
        {
            if(neededAmount <= 0) return;
            var remainingAmount = (neededAmount - currAmount);
            neededAmountText.text = remainingAmount < 0 ? "0" : dataContainer.walletManager.GetStringFormatByAmount(remainingAmount);
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, (float)currAmount / neededAmount, .1f);
            if(isDone || doneForNow) return;
            
            if (currAmount >= neededAmount)
            {
                if(PlayerPrefs.GetInt("Haptic").Equals(1)){MMVibrationManager.Haptic(HapticTypes.Success);}

                doneForNow = true;
                foreach (var e in onDoneEvents) { e.Invoke(); }
            }

            stayingTimer += Time.deltaTime;
            
            if (startTimer && dataContainer.walletManager.IsThereAnyMoney(currencyType, singularUnitAmount))
            {
                timer += Time.deltaTime;
                isObtaining = timer >= 1f;
            }
            else
            {
                isObtaining = false;
            }
            
            if (isObtaining && dataContainer.walletManager.IsThereAnyMoney(currencyType, singularUnitAmount))
            {
                CheckAreaOpen();
            }
        }

        public void SetHeaderText(string s)
        {
            headerText.text = s;
        }

        private Tweener cornerTween;
        private bool isCooldown;
        private async void CheckAreaOpen()
        {
            if (isCooldown) {return;}
            isCooldown = true;

            var flooredAmount = Mathf.FloorToInt(stayingTimer);
            if (flooredAmount > (int)((neededAmount - currAmount) / (float)singularUnitAmount))
            {
                flooredAmount = (int)((neededAmount - currAmount) / (float)singularUnitAmount);
            }

            if (!dataContainer.walletManager.IsThereAnyMoney(currencyType, (flooredAmount * singularUnitAmount)))
            {
                flooredAmount = dataContainer.walletManager.GetCurrencyAmount(currencyType) / singularUnitAmount;
            }
            
            for (var i = 0; i < flooredAmount; i++)
            {
                GameEvents.CurrencyReduce(currencyType, singularUnitAmount);
                currAmount += singularUnitAmount;
                
                var money = AquaPoolManager.PoolInit().GetObject(currencyPool);
                
                money.transform.position = player.position + (Vector3.up * 2f);
                money.transform.localScale = Vector3.one;
                money.transform.DOJump(claimPoint.position, 3f, 1, .2f).OnComplete(() =>
                {
                    ClaimedMoney(money);
                });
            }

            await UniTask.Delay(TimeSpan.FromSeconds(.1f / (stayingTimer)));
            isCooldown = false;
        }

        private void ClaimedMoney(GameObject go)
        {
            AquaPoolManager.PoolInit().ReturnObjectToPool(currencyPool, go, 0);
        }

        private float timer;
        private bool startTimer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                stayingTimer = 0;
                startTimer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                stayingTimer = 0;
                startTimer = false;
                doneForNow = false;
                timer = 0;
            }
        }

        private void OnDisable()
        {
            PlayerPrefs.SetInt(transform.parent.name + interactionId, currAmount);
        }
    }
}