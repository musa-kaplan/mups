using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MusaUtils.Pooling;
using MusaUtils.Templates.ArcadeIdle.General;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MusaUtils.Templates.ArcadeIdle.EnvironmentalScripts
{
    public class CurrencyClaimBehaviours : MonoBehaviour
    {
        [SerializeField] private InfoClasses.CurrencyType currencyType;

        private DataContainer dataContainer;

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
        }

        public void Claim(Transform pos)
        {
            dataContainer ??= DataContainer.dataContainer;
            ClaimingRitual(pos);
        }

        private async void ClaimingRitual(Transform pos)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.01f, 0.1f)));

            transform.DOJump(pos.position, 2f, 1, .2f);
            transform.DOScale(Vector3.zero, .25f);

            await UniTask.Delay(TimeSpan.FromSeconds(.25f));
            
            GameEvents.CurrencyIncrease(currencyType, dataContainer.walletManager.GetCurrency(currencyType).singularUnitAmount);
            AquaPoolManager.PoolInit().ReturnObjectToPool(
                dataContainer.walletManager.GetCurrency(currencyType).currencyPool, 
                this.gameObject, 0);
        }
    }
}