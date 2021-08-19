using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QuestGoal
{
    EnterArea, TakeItem, TalkTo, TalkedTo, GiveItem, DropItem, UseItem, UseEntity, ChangeLocation, HasItem, LookAt, ThoughtShowed
}

[System.Serializable]
public class Quest
{
    public QuestInfo info;
    public UnityEvent OnStart;
  

    public UnityEvent OnEnd;


}
