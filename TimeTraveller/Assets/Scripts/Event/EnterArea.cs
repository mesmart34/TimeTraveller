using UnityEngine.Events;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private UnityEvent onEnterTrigger;

    private void Start() {
        EventManager.instance.onTriggerAreaEnter += OnAreaEnter;
    }

    private void OnAreaEnter(int id, GameObject player)
    {
        if(this.id == id)
        {
            onEnterTrigger.Invoke();
        }
    }

    private void OnDestroy() {
        EventManager.instance.onTriggerAreaEnter -= OnAreaEnter;
    }
}
