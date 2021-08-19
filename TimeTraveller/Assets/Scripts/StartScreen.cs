using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    [TextArea] public string text;
    private bool skipped;
    [SerializeField] UnityEvent OnStartScreenEnded;

    private void Start() {
        StartCoroutine(TypeText());
        EventManager.instance.SetPlayerBusy(true);
    }

    private IEnumerator TypeText()
    {
        foreach(var c in text)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Skip()
    {
        if(!skipped)
        {
            StopAllCoroutines();
            textMesh.text = text;
            skipped = true;
        } else {
            gameObject.SetActive(false);
            EventManager.instance.SetPlayerBusy(false);
            OnStartScreenEnded?.Invoke();
        }
    }
}
