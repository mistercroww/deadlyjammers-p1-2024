using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public TMP_Text interactionText;
    private void Awake() {
        instance = this;
    }
    public void SetInteractionTextState(bool b, InteractableType interactType = InteractableType.Default) {
        switch (interactType) {
            case InteractableType.Default:
                interactionText.text = "[E] Interact";
                break;
            case InteractableType.Item:
                interactionText.text = "[E] Pick up";
                break;
            case InteractableType.ItemReceiver:
                interactionText.text = "[E] Use Item";
                break;
            case InteractableType.Switch:
                interactionText.text = "[E] Switch";
                break;
            case InteractableType.Door:
                interactionText.text = "[E] Open/Close";
                break;
        }
        interactionText.gameObject.SetActive(b);
    }
}
