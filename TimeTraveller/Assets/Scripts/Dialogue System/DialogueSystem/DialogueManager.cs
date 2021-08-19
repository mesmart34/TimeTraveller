using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dialogue;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueGraph Graph;
    [SerializeField] private GameObject[] Answers;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private TextMeshProUGUI Name; 
    [SerializeField] private Image AvatarSprite;
    private Chat RootChat;

    public void SetDialogueGraph(DialogueGraph graph)
    {
        Graph = graph;
        for (var i = 0; i < Answers.Length; i++)
        {
            Answers[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
        RootChat = (Chat)Graph.nodes[0];
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if(RootChat == null)
            return;
        Text.text = RootChat.text;
        AvatarSprite.sprite = RootChat.character.Avatar;
        for (var i = 0; i < RootChat.answers.Count; i++)
        {
            var answerObject = Answers[i];
            answerObject.SetActive(true);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = RootChat.answers[i].text;
            var option = i;
            answerObject.GetComponent<Button>().onClick.AddListener(()=>{
                OnButtonPressed(option);
            });
        }
    }
    
    private void OnButtonPressed(int option)
    {
        var temp = RootChat;
        RootChat.AnswerQuestion(option);
        RootChat = Graph.current;
        for (var i = 0; i < Answers.Length; i++)
        {
            Answers[i].SetActive(false);
            Answers[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
        ShowDialogue();
    }
}
