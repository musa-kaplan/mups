using System;
using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.Templates.HyperCasual
{
    [Serializable] public class SoundSources { public SoundType soundType; public AudioSource audioSource; }
    public enum SoundType{ButtonClick, MoneyClaim}
    public class HyperSoundManager : MonoBehaviour
    {
        private static event Action<SoundType> onSoundPlay;
        public static void SoundPlay(SoundType st) => onSoundPlay?.Invoke(st);

        [SerializeField] private List<SoundSources> soundSourcesByType;

        private void PlaySound(SoundType sType)
        {
            if(PlayerPrefs.GetInt("Sound").Equals(0)) return;
            foreach (var sSource in soundSourcesByType)
            {
                if (!sSource.soundType.Equals(sType)) continue;
                sSource.audioSource.Play();
                break;
            }
        }

        private void OnEnable() => onSoundPlay += PlaySound;

        private void OnDisable() => onSoundPlay -= PlaySound;
    }
}
