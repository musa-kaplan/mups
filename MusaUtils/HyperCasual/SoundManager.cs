using System;
using UnityEngine;

namespace MusaUtils.Templates.HyperCasual
{
    public class SoundManager : MonoBehaviour
    {
        /// <param>
        ///     <name>YOUR CODES</name>
        /// </param>
        
        private void StateHandler(GameStates a)
        {
            
        }

        private void OnEnable()
        {
            GameManager.onStateChanged += StateHandler;
        }

        private void OnDisable()
        {
            GameManager.onStateChanged -= StateHandler;
        }
    }
}
