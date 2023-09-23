using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.Templates.BasicQuestSystem.General
{
    [CreateAssetMenu(fileName = "NewQuestList", menuName = "MU/Quest List Data")]
    public class QuestData : ScriptableObject
    {
        public List<SingularQuestData> quests;

        public void Save()
        {
            for (var i = 0; i < quests.Count; i++)
            {
                PlayerPrefs.SetInt(quests[i].questName + "Progress", quests[i].currentAmount);
                PlayerPrefsExtra.SetBool(quests[i].questName + "IsDone", quests[i].isDone);
                PlayerPrefsExtra.SetBool(quests[i].questName + "IsClaimed", quests[i].isClaimed);
            }
        }

        public void Load()
        {
            for (var i = 0; i < quests.Count; i++)
            {
                if (PlayerPrefs.HasKey(quests[i].questName + "Progress"))
                { quests[i].currentAmount = PlayerPrefs.GetInt(quests[i].questName + "Progress");}
                else { quests[i].currentAmount = 0;}

                if (PlayerPrefs.HasKey(quests[i].questName + "IsDone"))
                { quests[i].isDone = PlayerPrefsExtra.GetBool(quests[i].questName + "IsDone");}
                else { quests[i].isDone = false;}

                if (PlayerPrefs.HasKey(quests[i].questName + "IsClaimed"))
                { quests[i].isClaimed = PlayerPrefsExtra.GetBool(quests[i].questName + "IsClaimed");}
                else { quests[i].isClaimed = false;}
                
            }
        }

        public SingularQuestData GiveQuestByIndex(int index)
        {
            for (var i = 0; i < quests.Count; i++)
            {
                if (quests[i].id.Equals(index))
                {
                    return quests[i];
                }
            }

            return null;
        }
        
        public SingularQuestData GiveQuestByName(string questName)
        {
            for (var i = 0; i < quests.Count; i++)
            {
                if (quests[i].questName.Equals(questName))
                {
                    return quests[i];
                }
            }

            return null;
        }

        public List<SingularQuestData> GiveActiveQuests()
        {
            var activeQuests = new List<SingularQuestData>();
            for (var i = 0; i < quests.Count; i++)
            {
                if (!quests[i].isDone)
                {
                    activeQuests.Add(quests[i]);
                }
            }

            return activeQuests;
        }

        public List<SingularQuestData> GiveCompletedQuests()
        {
            var completedQuests = new List<SingularQuestData>();
            for (var i = 0; i < quests.Count; i++)
            {
                if (quests[i].isDone)
                {
                    completedQuests.Add(quests[i]);
                }
            }

            return completedQuests;
        }
    }
}
