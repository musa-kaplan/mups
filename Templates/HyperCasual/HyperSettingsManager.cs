using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MusaUtils.Templates.HyperCasual
{
    public class HyperSettingsManager: MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;

        [SerializeField] private Button hapticButton;
        [SerializeField] private Button soundButton;

        [SerializeField] private List<Sprite> hapticSprites;
        [SerializeField] private List<Sprite> soundSprites;
        
        private bool isHaptic;
        private bool isSound;
        private bool isSettingsOpened;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Sound"))
            {PlayerPrefs.SetInt("Sound", 1);}
            
            if (!PlayerPrefs.HasKey("Haptic"))
            {PlayerPrefs.SetInt("Haptic", 1);}
        }

        private void Start()
        {
            isHaptic = PlayerPrefs.GetInt("Haptic").Equals(1);
            isSound = PlayerPrefs.GetInt("Sound").Equals(1);
            
            SetImages();
        }

        public void SettingsButton()
        {
            _settingsButton.interactable = false;
            isSettingsOpened = !isSettingsOpened;

            hapticButton.interactable = soundButton.interactable = _settingsButton.interactable = false;
            
            hapticButton.transform.DOScale(isSettingsOpened ? Vector3.one : Vector3.zero, .5f);
            soundButton.transform.DOScale(isSettingsOpened ? Vector3.one : Vector3.zero, .5f).OnComplete(() =>
            {
                hapticButton.interactable = soundButton.interactable = _settingsButton.interactable = true;
            });
        }

        public void HapticButton()
        {
            isHaptic = !isHaptic;
            PlayerPrefs.SetInt("Haptic", isHaptic ? 1 : 0);
            SetImages();
        }

        public void SoundButton()
        {
            isSound = !isSound;
            PlayerPrefs.SetInt("Haptic", isSound ? 1 : 0);
            SetImages();
        }

        private void SetImages()
        {
            hapticButton.image.sprite = hapticSprites[isHaptic ? 1 : 0];
            soundButton.image.sprite = soundSprites[isSound ? 1 : 0];
        }
    }
}
