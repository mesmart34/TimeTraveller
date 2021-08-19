using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseButton : MonoBehaviour
{
    [SerializeField] private float range = 0.025f;
    [SerializeField] private float waitTime = 0.1f;
    [SerializeField] public UnityEvent onClickEvent;
    [SerializeField] private bool isClickable = true;
    [SerializeField] private Gradient darkGradient;
    private Gradient lightGradient;
    private Vector3 initPosition;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private bool busy;

    private void Awake() {
        initPosition = transform.position;
        lineRenderer = GetComponentInChildren<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightGradient = lineRenderer.colorGradient;
    }

    private void Start() {
        EventManager.instance.onPlayerBusyStart += OnEventSystemBusyStart;
        EventManager.instance.onPlayerBusyEnd += OnEventSystemBusyEnd;
    }

    private void OnEnable() {
        StartCoroutine(SetRandomPosition());
    }

    private void OnEventSystemBusyStart()
    {
        busy = true;
    }

    private void OnEventSystemBusyEnd()
    {
        busy = false;
    }

    public void OnMouseDown() 
    {
        if(isClickable && !busy)
            onClickEvent.Invoke();
    }

    private void OnMouseEnter() {
        if(busy)
            return;
        var color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        lineRenderer.colorGradient = darkGradient;
    }

    private void OnMouseExit() {
        if(busy)
            return;
        var color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;
        lineRenderer.colorGradient = lightGradient;
    }
    
    private IEnumerator SetRandomPosition()
    {
        while(true)
        {
            var pos = new Vector2(
                Random.Range(initPosition.x - range, initPosition.x + range),
                Random.Range(initPosition.y - range, initPosition.y + range
            ));
            transform.position = pos;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnDestroy() {
        StopAllCoroutines();
        EventManager.instance.onPlayerBusyStart -= OnEventSystemBusyStart;
        EventManager.instance.onPlayerBusyEnd -= OnEventSystemBusyEnd;
    }
}
