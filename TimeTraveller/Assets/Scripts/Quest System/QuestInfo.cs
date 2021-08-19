using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "QuestSystem/Quest", order = 1)]
public class QuestInfo : ScriptableObject
{
    public string Name;
    public QuestGoal goal;
    //public int areaId;
    public int value;
}
