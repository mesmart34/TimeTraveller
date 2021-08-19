using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSwitcher : MonoBehaviour
{
    [SerializeField] private Light2D light2D = null;
    [SerializeField] private int id;

    private void Start() {
        EventManager.instance.onLightSwitcherUse += Use;
    }

    public void Use(int id, GameObject player)
    {
        if(this.id == id)
        {
            var playerController = player.GetComponent<PlayerController>();
            playerController.SetTarget(transform.position);
            playerController.SetArrivalEvent(()=>{
                light2D.enabled = !light2D.enabled;
            });
        }
    }

    private void OnDestroy() {
        EventManager.instance.onLightSwitcherUse -= Use;
    }
}
