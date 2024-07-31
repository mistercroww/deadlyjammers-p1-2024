using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public string tagToCheck;
    public bool isOneShot;
    public UnityEvent eventsToTrigger;
    bool triggered;

    public void TriggerEvents() {
        if (isOneShot && triggered) return;
        if(eventsToTrigger != null) {
            triggered = true;
            eventsToTrigger.Invoke();
        }
    }
    private void OnTriggerEnter(Collider o) {
        if (o.CompareTag(tagToCheck)) {
            TriggerEvents();
        }
    }
}
