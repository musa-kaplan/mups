using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MusaUtils.Templates.ArcadeIdle.General;
using MusaUtils.Templates.BasicQuestSystem.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MusaUtils.Templates.BasicQuestSystem.Ui
{
    public class QuestUiElement : MonoBehaviour
    {
        [NonSerialized] public SingularQuestData questData;
        [NonSerialized] public RectTransform target;
        [NonSerialized] public Transform releasedParent;
        
        [SerializeField] private TextMeshProUGUI questNameText;
        [SerializeField] private TextMeshProUGUI prizeAmountText;
        [SerializeField] private TextMeshProUGUI claimText;
        [SerializeField] private Image prizeCurrencyImage;
        [SerializeField] private Image currentAmountFillImage;
        [SerializeField] private Button claimButton;
        
        [Header("Claiming Visuals")]
        [SerializeField] private List<Image> claimableCurrencyImages;
        [SerializeField] private AnimationCurve bumpCurve;
        [SerializeField] private AnimationCurve[] curves;

        private DataContainer _dataContainer;

        private void LateUpdate()
        {
            if (questData != null)
            {
                currentAmountFillImage.fillAmount = questData.isDone ? 1f : Mathf.Lerp(currentAmountFillImage.fillAmount,
                    (questData.currentAmount / (float)questData.neededAmount), .35f);

                claimButton.interactable = (questData.isDone && !questData.isClaimed);
            }
        }

        private async void DelayedSetElements()
        {
            await UniTask.DelayFrame(1);
            _dataContainer ??= DataContainer.dataContainer;
            if (questData == null) return;
            questNameText.text = questData.questName;
            prizeAmountText.text = questData.prizeAmount.ToString();
            prizeAmountText.gameObject.SetActive(!questData.isClaimed);
            prizeCurrencyImage.sprite = _dataContainer.walletManager.GetCurrency(questData.prizeCurrencyType).currencySprite;
            prizeCurrencyImage.gameObject.SetActive(!questData.isClaimed);
            
            claimText.gameObject.SetActive(questData.isClaimed);
        }

        private void ClaimButton()
        {
            questData.isClaimed = true;
            GameEvents.CurrencyIncrease(questData.prizeCurrencyType, questData.prizeAmount);
            SpreadCurrencies();
            
            DelayedSetElements();
        }

        private async void SpreadCurrencies()
        {
            for (var i = 0; i < 10; i++)
            {
                var img = claimableCurrencyImages.FromList();
                claimableCurrencyImages.Remove(img);
                img.sprite = _dataContainer.walletManager.GetCurrency(questData.prizeCurrencyType).currencySprite;
                img.transform.localScale = Vector3.zero;
                img.rectTransform.position = prizeCurrencyImage.rectTransform.position;
                img.gameObject.SetActive(true);
                Spreading(img);
                
                await UniTask.Delay(TimeSpan.FromSeconds(.1f));
            }
        }

        private async void Spreading(Image img)
        {
            img.transform.SetParent(releasedParent);
            img.transform.DOScale(Vector3.one, .25f).SetEase(bumpCurve);
            img.rectTransform.DOMoveX(prizeCurrencyImage.rectTransform.position.x + Random.Range(-100f, 100f), .45f);
            img.rectTransform.DOMoveY(prizeCurrencyImage.rectTransform.position.y + Random.Range(-100f, 100f), .45f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            
            img.rectTransform.DOMoveX(target.position.x, .4f).SetEase(curves.FromArray());
            img.rectTransform.DOMoveY(target.position.y, .4f).SetEase(curves.FromArray());

            await UniTask.Delay(TimeSpan.FromSeconds(.45f));
            img.gameObject.SetActive(false);
            claimableCurrencyImages.Add(img);
            img.transform.SetParent(transform);
        }
        
        private void OnEnable()
        {
            _dataContainer = DataContainer.dataContainer;
            
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(ClaimButton);
            
            for (var i = 0; i < claimableCurrencyImages.Count; i++)
            {
                claimableCurrencyImages[i].transform.localScale = Vector3.zero;
                claimableCurrencyImages[i].gameObject.SetActive(false);
            }
            DelayedSetElements();
        }
    }
}
