using MusaUtils.Templates.ArcadeIdle.General;
using UnityEngine;

namespace MusaUtils.Templates.BasicQuestSystem.General
{
    [CreateAssetMenu(fileName = "NewSingularQuest", menuName = "MU/Singular Quest Data")]
    public class SingularQuestData : ScriptableObject
    {
        public int id;
        public string questName;
        
        [Header("Quest State")]
        public int neededAmount;
        public int currentAmount;
        public bool isDone;
        
        [Header("Prize")]
        public InfoClasses.CurrencyType prizeCurrencyType;
        public int prizeAmount;
        public bool isClaimed;
    }
}