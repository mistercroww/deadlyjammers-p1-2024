using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable {
    public bool isDoorEnabled = true;
    public bool IsInteractable() {
        return isDoorEnabled;
    }
    public InteractableType InteractionType() {
        return InteractableType.Door;
    }
    public void TriggerInteraction() {
        if (!isDoorEnabled) return;
        LoadNextLevel();
    }
    public void LoadNextLevel() {
        print("PASASTE EL DIA MACACO");
    }
}
