using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class SubtitleSystem : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public GameObject SubtitleUI;
    [SerializeField] float timeBetweenLetters = 0.1f;
    [SerializeField] float timeBetweenSentences = 1.0f;

    private SubtitleTrack currentTrack;

    public event Action<int> OnSubtitleTrackTyped;

    public void SetSubtitleTrack(SubtitleTrack track)
    {
        currentTrack = track;
    }

    public void ShowSubtitles()
    {
        SubtitleUI.SetActive(true);
        textMesh.text = "";
        StartCoroutine(TypeSubtitleTrack());
    }

    private IEnumerator TypeSubtitleTrack()
    {
        foreach (var sentence in currentTrack.Sentences)
        {
            foreach (var c in sentence)
            {
                textMesh.text += c;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            yield return new WaitForSeconds(timeBetweenSentences);
            textMesh.text = "";
        }
        OnSubtitleTrackTyped?.Invoke(currentTrack.id);
        SubtitleUI.SetActive(false);
    }
}
