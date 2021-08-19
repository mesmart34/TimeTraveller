using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform otherTeleporter;
    [SerializeField] private CinemachineVirtualCamera current;
    [SerializeField] private CinemachineVirtualCamera other;
    [SerializeField] private int id;

    private void Start() {
        EventManager.instance.onTeleportUse += Use;
    }

    public void Use(int id, GameObject player)
    {
        if(this.id == id)
        {
            var playerController = player.GetComponent<PlayerController>();
            playerController.SetArrivalEvent(()=>{
                ScreenFader.instance.Fade(()=>{
                    playerController.transform.position = otherTeleporter.position;
                    current.enabled = !current.enabled;
                    other.enabled = !other.enabled;
                    playerController.SetTarget(playerController.transform.position);
                });
            });
        }
    }

    private void OnDestroy() {
        EventManager.instance.onTeleportUse -= Use;
    }
}
