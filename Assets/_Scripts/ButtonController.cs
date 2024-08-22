using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour, IInteractable
{

    public bool isInteractable = true;
    public UnityEvent buttonInteractionEvent;
    public bool IsInteractable()
    {
        return isInteractable;
    }

    public void TriggerInteraction()
    {
        if (buttonInteractionEvent != null)
        {
            buttonInteractionEvent.Invoke();
        }
    }

    public InteractableType InteractionType()
    {
        return InteractableType.Default;
    }
}
