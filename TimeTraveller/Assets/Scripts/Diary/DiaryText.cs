using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Diary Text", menuName = "Diary/DiaryText", order = 1)]
public class DiaryText : ScriptableObject
{
    [TextArea] public List<string> Texts;
}
