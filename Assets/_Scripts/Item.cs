using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable {
    public ItemSO itemSO;
    public bool CanBePicked = true;
    public Collider itemCollider;

    public void SetCollisionState(bool b) {
        itemCollider.enabled = b;
    }
    public bool IsInteractable() {
        return CanBePicked;
    }
    public void TriggerInteraction() {
        if (!CanBePicked) return;
        CanBePicked = false;
        PlayerController.instance.PickupItem(itemSO);
        Destroy(this.gameObject);
    }
    public InteractableType InteractionType() {
        return InteractableType.Item;
    }
}
