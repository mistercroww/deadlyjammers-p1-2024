using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerMovement playerMovement;
    public Transform handTransform;
    public Item currentItemInHand;

    private void Awake() {
        instance = this;
    }
    private void Update() {
        InteractionCheck();
    }

    private void InteractionCheck() {
        if (Input.GetKeyDown(KeyCode.E)) {
            int lm = 1 << 6;
            lm = ~lm;
            if (Physics.SphereCast(
                Camera.main.transform.position, 0.1f, Camera.main.transform.forward, out RaycastHit hit, 1.5f, lm)) {
                if (hit.collider.CompareTag("Interactable")) {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null) {
                        if (interactable.IsInteractable()) {
                            interactable.TriggerInteraction();
                        }
                    }
                }
            }
        }
    }
    public void PickupItem(ItemSO newItem) {
        //var tItem = Instantiate(newItem.itemPrefab, handTransform);
        currentItemInHand = Instantiate(newItem.itemPrefab, handTransform).GetComponent<Item>();
        currentItemInHand.transform.localPosition = Vector3.zero;
        currentItemInHand.transform.localEulerAngles = Vector3.zero;
        currentItemInHand.SetCollisionState(false);
    }
}
