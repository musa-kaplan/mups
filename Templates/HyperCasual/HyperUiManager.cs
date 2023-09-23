using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MusaUtils.Templates.HyperCasual
{
    public class HyperUiManager : MonoBehaviour
    {
        /// <param>
        ///     <name>YOUR CODES</name>
        /// </param>

        [SerializeField] private List<Canvasses> _canvasses;

        #region BUTTONS

        public void StartButton()
        {
            HyperGameManager.ChangeState(GameStates.Started);
        }
        
        public void NextButton()
        {
            //YOUR NEXT SCENE CODES
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RetryButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
        
        private void StateHandler(GameStates a)
        {
            foreach (var canvas in _canvasses)
            {
                canvas._canvas.SetActive(canvas._state.Equals(a));
            }
        }

        private void OnEnable()
        {
            HyperGameManager.onStateChanged += StateHandler;
        }

        private void OnDisable()
        {
            HyperGameManager.onStateChanged -= StateHandler;
        }
    }

    [Serializable]
    public class Canvasses
    {
        public GameStates _state;
        public GameObject _canvas;
    }
}
