using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using MusaUtils.Pooling;
using MusaUtils.Templates.ArcadeIdle.General;
using MusaUtils.Templates.HyperCasual;
using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.EnvironmentalScripts
{
    public class CurrencyClaimArea : MonoBehaviour
    {
        [SerializeField] private InfoClasses.CurrencyType currencyType;
        [SerializeField] private Vector3 lineHeightColumn;
        [SerializeField] private Vector3 moneyScale;

        private DataContainer dataContainer;
        private List<CurrencyClaimBehaviours> moneys = new List<CurrencyClaimBehaviours>();
        private List<Vector3> moneyPositions = new List<Vector3>();

        private int internalMoneyCount;
        private int singularUnitAmount;

        private void Start()
        {
            dataContainer = DataContainer.dataContainer;
            singularUnitAmount = dataContainer.walletManager.GetCurrency(currencyType).singularUnitAmount;
            SetPositions();
        }

        private void SetPositions()
        {
            moneyPositions.Clear();
            var cPos = transform.position;
            for (var y = 0; y < lineHeightColumn.y; y++)
            {
                for (var z = 0; z < lineHeightColumn.z; z++)
                {
                    for (var x = 0; x < lineHeightColumn.x; x++)
                    {
                        moneyPositions.Add(new Vector3((cPos.x -
                                                        ((x * moneyScale.x) -
                                                         ((lineHeightColumn.x / 2f) * moneyScale.x) +
                                                         (moneyScale.x / 2f))),
                            cPos.y + (y * moneyScale.y), (cPos.z -
                                                          ((z * moneyScale.z) -
                                                           ((lineHeightColumn.z / 2f) * moneyScale.z) +
                                                           (moneyScale.z / 2f)))));
                    }
                }
            }
        }

        public async void PourMoney(Vector3 pos, Vector3 rot)
        {
            internalMoneyCount++;
            var money = AquaPoolManager.PoolInit()
                .GetObject(dataContainer.walletManager.GetCurrency(currencyType).currencyPool);

            money.transform.position = pos;
            money.transform.rotation = Quaternion.Euler(rot);

            var moneyPos = internalMoneyCount < moneyPositions.Count
                ? moneyPositions[internalMoneyCount - 1]
                : moneyPositions[^1];

            money.transform.DOJump(moneyPos, 2f, 1, .2f);
            money.transform.DORotate(Vector3.zero, .2f);

            await UniTask.Delay(TimeSpan.FromSeconds(.2f));

            if (money.TryGetComponent(out CurrencyClaimBehaviours moneyBehaviours))
            {
                moneys.Add(moneyBehaviours);
            }

            SetMoneyPositions();
        }

        private void SetMoneyPositions()
        {
            for (var i = 0; i < moneys.Count; i++)
            {
                moneys[i].transform.position = i < moneyPositions.Count ? moneyPositions[i] : moneyPositions[^1];
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            var totalMoneyAmount = moneys.Count * singularUnitAmount;
            if (totalMoneyAmount > 0)
            {
                GameEvents.MoneyEarnedUi(totalMoneyAmount);
                HyperSoundManager.SoundPlay(SoundType.MoneyClaim);
                if (PlayerPrefs.GetInt("Haptic").Equals(1)){MMVibrationManager.Haptic(HapticTypes.MediumImpact);}
                
                for (var i = 0; i < moneys.Count; i++)
                {
                    moneys[i].Claim(other.transform);
                }

                moneys.Clear();
                internalMoneyCount = 0;
            }
        }
    }
}