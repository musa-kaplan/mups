using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using MusaUtils.Templates.BasicQuestSystem.Ui;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MusaUtils.Templates.BasicQuestSystem.General
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class QuestManager : MonoBehaviour
    {
        #region EVENTS

        public static event Action<int, string, int> onQuestProgress;
        public static void QuestProgress(int questIdentifier, [CanBeNull]string questName, int increaseAmount) =>
            onQuestProgress?.Invoke(questIdentifier, questName = null, increaseAmount);

        #endregion

        [SerializeField] private QuestData questListData;
        [SerializeField] private GameObject questUiElementPrefab;
        
        [Header("Visuals")]
        [SerializeField] private RectTransform generalCanvas;
        [SerializeField] private RectTransform contentRect;
        [SerializeField] private RectTransform generalCurrencyImagePosition;
        [SerializeField] private Button questButton;

        private SingularQuestData tempQuestData;
        private List<QuestUiElement> uiElements = new List<QuestUiElement>();
        private bool isWindowOpen;
        private Tweener windowTweener;

        private void Start()
        {
            if (TryGetComponent(out CanvasScaler cs))
            {
                cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                cs.referenceResolution = new Vector2(1080, 1920);
                cs.matchWidthOrHeight = .5f;
            }
            
            if (TryGetComponent(out Canvas c))
            { c.renderMode = RenderMode.ScreenSpaceOverlay;}
            
            contentRect.anchorMin = new Vector2(0, .5f - (questListData.quests.Count * .15f));
            contentRect.anchorMax = new Vector2(1f, .5f + (questListData.quests.Count * .15f));
            
            contentRect.anchoredPosition = Vector2.zero;

            for (var i = 0; i < questListData.quests.Count; i++)
            {
                var e = Instantiate(questUiElementPrefab, contentRect);
                if (e.TryGetComponent(out QuestUiElement el))
                {
                    uiElements.Add(el);
                }
            }
        }

        private void SetTheList()
        {
            var qList = questListData.GiveActiveQuests();
            for (var i = 0; i < qList.Count; i++)
            {
                uiElements[i].target = generalCurrencyImagePosition;
                uiElements[i].questData = qList[i];
                uiElements[i].releasedParent = transform;
            }

            var qListCompleted = questListData.GiveCompletedQuests();
            int internalIndex = 0;
            for (var i = qList.Count; i < questListData.quests.Count; i++)
            {
                uiElements[i].target = generalCurrencyImagePosition;
                uiElements[i].questData = qListCompleted[internalIndex];
                uiElements[i].releasedParent = transform;
                internalIndex++;
            }
        }

        private void QuestButtonAction()
        {
            isWindowOpen = !isWindowOpen;
            if (isWindowOpen)
            {
                generalCanvas.gameObject.SetActive(true);
                SetTheList();
            }
            WindowAction();
        }

        private async void WindowAction()
        {
            questButton.interactable = false;
            windowTweener?.Kill();
            windowTweener = generalCanvas.DOScale(isWindowOpen ? Vector3.one : Vector3.zero, .15f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            generalCanvas.gameObject.SetActive(isWindowOpen);
            questButton.interactable = true;
        }

        private void ProgressingQuest(int questIndex, string questName, int amount)
        {
            tempQuestData = questName != null ? questListData.GiveQuestByName(questName) : questListData.GiveQuestByIndex(questIndex);
            tempQuestData.currentAmount += amount;
            tempQuestData.isDone = tempQuestData.currentAmount >= tempQuestData.neededAmount;
        }
        
        private void OnEnable()
        {
            generalCanvas.localScale = Vector3.zero;
            generalCanvas.gameObject.SetActive(false);
            questButton.onClick.RemoveAllListeners();
            questButton.onClick.AddListener(QuestButtonAction);
            
            questListData.Load();
            onQuestProgress += ProgressingQuest;
        }

        private void OnDisable()
        {
            questListData.Save();
            onQuestProgress -= ProgressingQuest;
        }
    }
}
