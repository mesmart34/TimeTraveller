using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveObject : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera current;
    [SerializeField] private CinemachineVirtualCamera other;

    public void ChangePosition(Transform point)
    {
        StartCoroutine(ChangePositionIEnumerator(point));
    }

    private IEnumerator ChangePositionIEnumerator(Transform point)
    {
        yield return new WaitForSeconds(0.5f);
        current.enabled = false;
        other.enabled = true;
        transform.position = point.position;
        GetComponent<PlayerController>().SetTarget(point.position);
    }
}
