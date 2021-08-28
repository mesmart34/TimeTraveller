using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private string colliderTag;
    [SerializeField] private UnityEvent onEnterTrigger;

    private void OnTriggerEnter2D(Collider2D other) {
        print(other.gameObject.name);
        if(other.gameObject.tag == colliderTag)
        {
            onEnterTrigger.Invoke();
        }
    }
}
