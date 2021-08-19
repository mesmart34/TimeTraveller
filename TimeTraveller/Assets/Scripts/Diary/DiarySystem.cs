using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiarySystem : MonoBehaviour
{
    public GameObject DiaryUI;
    private DiaryText text;
    public TextMeshProUGUI textMesh;
    private int currentPage;
    public TextMeshProUGUI pageText;
    
    public void ShowPage()
    {
        textMesh.text = text.Texts[currentPage];
        pageText.text = (currentPage + 1).ToString() + "/" + text.Texts.Count.ToString();
    }

    public void Left()
    {
        if(currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }
    
    public void Right()
    {
        if(currentPage < text.Texts.Count - 1)
        {
            currentPage++;
            ShowPage();
        }
    }

    public void SetDiaryText(DiaryText text)
    {
        this.text = text;
    }

    public void Open()
    {
        DiaryUI.SetActive(true);
        ShowPage();
    }

    public void Close()
    {
        DiaryUI.SetActive(false);
    }
}
