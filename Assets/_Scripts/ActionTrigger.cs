using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour
{
    public bool isOneShot;
    public UnityEvent actionEvents;
    bool triggered;

    public void TriggerEvents() {
        if (isOneShot && triggered) return;
        if(actionEvents != null) {
            actionEvents.Invoke();
            triggered = true;
        }
    }
}
