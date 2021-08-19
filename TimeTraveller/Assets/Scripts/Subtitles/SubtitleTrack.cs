using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Subtitles", menuName = "Subtitles System/Subtitle Track", order = 1)]
public class SubtitleTrack : ScriptableObject
{
    public int id;
    public string[] Sentences;
}