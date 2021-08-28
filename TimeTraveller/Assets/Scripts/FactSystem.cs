using TMPro;
using System.Collections;
using UnityEngine;

public class FactSystem : MonoBehaviour
{
    [SerializeField] GameObject m_factSystemUI;
    [SerializeField] TextMeshProUGUI m_textMesh;

    public void Open()
    {
        m_factSystemUI.SetActive(true);
    }

    public void SetText(SubtitleTrack track)
    {
        m_textMesh.text = track.Sentences[0];
    }

    public void Close()
    {
        m_factSystemUI.SetActive(false);
    }

}
