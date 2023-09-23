using System;
using MusaUtils.Templates.BasicQuestSystem.General;
using UnityEngine;

namespace MusaUtils.Templates.BasicQuestSystem.DEMO
{
    public class DemoQuestCompleter : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                QuestManager.QuestProgress(1, null, 1);
            }
        }
    }
}
