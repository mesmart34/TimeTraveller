using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject runningDust;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] public Animator animator = null;
    [SerializeField] public float magnitude = 1.0f;
    [SerializeField] public float minDistanceToTarget = 1.0f;
    public bool canMove = true;

    private CharacterController2D controller = null;
    private float move = 0.0f;
    private Rigidbody2D rb = null;
    private Vector2 target;
    private Action OnArrival = null;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        target = rb.transform.position;
    } 

    private void Stop()
    {
        move = 0.0f;
        target = transform.position;
        if(OnArrival != null)
        {
            target = transform.position;
            OnArrival.Invoke();
            OnArrival = null;
        }
        target = transform.position;
    }

    private IEnumerator OnArrivalEnumarator()
    {
        yield return new WaitForSeconds(1.1f);
        if(OnArrival != null)
        {
           
        }
    }

    public void CanMove(bool value)
    {
        canMove = value;
    }

    private bool IsNearTo(Vector2 target)
    {
        return Vector2.Distance(transform.position, target) < minDistanceToTarget;
    }

    public void SetArrivalEvent(Action e)
    {
        OnArrival = e;
    }
    
    public void SetTarget(Vector2 target) {
        this.target = target;
        this.target.y = transform.position.y;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canMove)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetTarget(target);
        }

        if(IsNearTo(target))
        {
            Stop();
        }
        else
        {
            Move();
        }

        if (rb.velocity.magnitude > magnitude)
        {
            animator.SetBool("Walk", true);
            runningDust.gameObject.SetActive(true);
        }
        else
        {
            runningDust.gameObject.SetActive(false);
            animator.SetBool("Walk", false);
        }
    }
    private void Move()
    {
       move = Mathf.Sign(target.x - transform.position.x);
    }

    private void FixedUpdate()
    {
        controller.Move(move * Time.fixedDeltaTime * speed);
    }
}