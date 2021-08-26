using System;
using UnityEngine;

namespace MusaUtils.Templates.HyperCasual
{
    public enum GameStates
    {
        Enterance,
        Started,
        Win,
        Lose
    }
    
    public class GameManager : MonoBehaviour
    {
        public static event Action<GameStates> onStateChanged;
        public static void ChangeState(GameStates a) => onStateChanged?.Invoke(a);
        
        public static GameManager _instance;

        public GameStates _states;

        private void Awake()
        {
            while (true)
            {
                _instance = this;
                if (_instance == null)
                {
                    continue;
                }

                break;
            }
        }
    }
}
