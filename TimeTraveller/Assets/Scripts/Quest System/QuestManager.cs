using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Quest[] quests;
    private Quest currentQuest = null;
    public Text questName;
    public int currentQuestId = 0;
    [SerializeField] private Inventory inventory;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        currentQuest = quests[currentQuestId];
        StartCurrentQuest();
    }

    public void PushEvent(QuestGoal goal, int value = 0) 
    {
        if(currentQuest == null)
            return;
        if(currentQuest.info.goal == goal)
        {
            if(currentQuest.info.value == value)
            {
                SetQuestDescription("");
                EndCurrentQuest();
            }
        }
    }

    public void SetQuestDescription(string text)
    {
        if(questName != null)
            questName.text = text;
    }

    public void NextQuest()
    {
        if(currentQuestId < quests.Length - 1)
        {
            currentQuestId++;
            currentQuest = quests[currentQuestId];
            StartCurrentQuest();
            SetQuestDescription(currentQuest.info.Name);
        }
        else
        {
            SetQuestDescription("");
        }

        currentQuest = quests[currentQuestId];
    }

    public void StartCurrentQuest()
    {
        SetQuestDescription(currentQuest.info.Name);
        currentQuest.OnStart.Invoke();
    }

    public void EndCurrentQuest()
    {
        SetQuestDescription("");
        currentQuest.OnEnd.Invoke();
        NextQuest();
    }
}
