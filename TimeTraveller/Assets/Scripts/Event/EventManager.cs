using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;
    public event Action<int, GameObject> onTeleportUse;
    public event Action<int, GameObject> onLightSwitcherUse;
    public event Action<int, GameObject> onTriggerAreaEnter;
    public event Action onPlayerBusyStart;
    public event Action onPlayerBusyEnd;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Inventory inventory;
    [SerializeField] private SubtitleSystem subtitleSystem;
    [SerializeField] private DiarySystem diarySystem;

    private void Awake() {
         if(instance == null)
            instance = this;
        subtitleSystem.OnSubtitleTrackTyped += OnSubtitleTrackEnd;
    }

    private void OnDestroy()
    {
        subtitleSystem.OnSubtitleTrackTyped -= OnSubtitleTrackEnd;
    }

    public void TakeItem(ItemBase item)
    {
        QuestManager.instance.PushEvent(QuestGoal.TakeItem, 0);
         var playerController = player.GetComponent<PlayerController>();
            playerController.SetTarget(transform.position);
            playerController.SetArrivalEvent(()=>{
                inventory.Add(item);
                item.destroyEvent.Raise();
            });
    }

    public void TriggerTeleport(int id)
    {
       QuestManager.instance.PushEvent(QuestGoal.ChangeLocation, id);
        if(onTeleportUse != null)
        {
            onTeleportUse(id, player);
        }
    }

    public void TriggerTeleportWithDelay(int id)
    {
        StartCoroutine(TeleportDelay(id));
        // QuestManager.instance.PushEvent(QuestGoal.ChangeLocation, id);
        // if(onTeleportUse != null)
        // {
        //     onTeleportUse(id, player);
        // }
    }

    private IEnumerator TeleportDelay(int id)
    {
        if(onTeleportUse != null)
        {
            onTeleportUse(id, player);
        }
        yield return new WaitForSeconds(2.0f);
        QuestManager.instance.PushEvent(QuestGoal.ChangeLocation, id);
    }

    public void TriggerLightSwitcher(int id)
    {
        if(onLightSwitcherUse != null)
        {
            onLightSwitcherUse(id, player);
        }
    }

    public void NextLevelWithDelay(float time)
    {
        StartCoroutine(LoadScene(time));
    }

    private IEnumerator LoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void SetPlayerBusy(bool value)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.CanMove(!value);
        if(value)
        {
            onPlayerBusyStart?.Invoke();

        } else {
            onPlayerBusyEnd?.Invoke();

        }
    }

    public void TriggerEnterArea(int id)
    {
        QuestManager.instance.PushEvent(QuestGoal.EnterArea, id);
        if(onTriggerAreaEnter != null)
        {
            onTriggerAreaEnter(id, player);
        }
    }    

    public void StartDialogue()
    {
        QuestManager.instance.PushEvent(QuestGoal.TalkTo, 0);
         var playerController = player.GetComponent<PlayerController>();
            playerController.SetTarget(transform.position);
            playerController.SetArrivalEvent(()=>{
                dialogueUI.SetActive(true);
                SetPlayerBusy(true);
            });
    }

    public void StartStaticDialogue()
    {
        QuestManager.instance.PushEvent(QuestGoal.TalkTo, 0);
        dialogueUI.SetActive(true);
        SetPlayerBusy(true);
    }

    public void EndDialgoue(bool alarmQuestSystem)
    {
        dialogueUI.SetActive(false);
        if(alarmQuestSystem)
            QuestManager.instance.PushEvent(QuestGoal.TalkedTo);
        SetPlayerBusy(false);

    }

    public void PlaySubtitleTrack(SubtitleTrack track)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.SetTarget(transform.position);
        playerController.SetArrivalEvent(() =>
        {
            subtitleSystem.SetSubtitleTrack(track);
            subtitleSystem.ShowSubtitles();
            SetPlayerBusy(true);
        });
    }

    private void OnSubtitleTrackEnd(int id)
    {
        SetPlayerBusy(false);
        QuestManager.instance.PushEvent(QuestGoal.LookAt, id);
    }

    public void OpenDiary(DiaryText diary)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.SetTarget(transform.position);
        playerController.SetArrivalEvent(() =>
        {
           SetPlayerBusy(true);
            diarySystem.SetDiaryText(diary);
            diarySystem.Open();
        });
    }

    public void CloseDiary()
    {
        diarySystem.Close();
       SetPlayerBusy(false);
    }
}
