using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerMovement playerMovement;
    public Transform handTransform;
    public Item currentItemInHand;
    public float sanity = 100;
    public Volume sanityVolume;
    public Animator clipboardAnim;

    private void Awake() {
        instance = this;
    }
    private void Update() {
        InteractionCheck();
        ClipboardAnimationHandler();

        //sanity
        sanityVolume.weight = 1f - (sanity / 100f);
    }

    private void ClipboardAnimationHandler() {
        if (clipboardAnim == null) return;
        clipboardAnim.SetBool("Check", Input.GetMouseButton(1));
    }

    private void InteractionCheck() {
        int lm = 1 << 6;
        lm = ~lm;

        UI_Manager.instance.SetInteractionTextState(false);

        if (Physics.SphereCast(
            Camera.main.transform.position, 0.1f, Camera.main.transform.forward, out RaycastHit hit, 1.5f, lm)) {
            if (hit.collider.CompareTag("Interactable")) {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null) {
                    if (interactable.IsInteractable()) {
                        if(interactable.InteractionType() == InteractableType.ItemReceiver && currentItemInHand == null) {
                            return;
                        }
                        UI_Manager.instance.SetInteractionTextState(true, interactable.InteractionType());
                        if (Input.GetKeyDown(KeyCode.E)) {
                            interactable.TriggerInteraction();
                        }
                    }
                }
            }
        }
    }
    public void ClearCurrentItem(bool dropItem) {
        if (currentItemInHand && dropItem) {
            Vector3 spawnPos = transform.position + (transform.forward.normalized * 0.5f);
            Instantiate(currentItemInHand.itemSO.itemPrefab, spawnPos, Quaternion.identity);
        }
        if(handTransform.childCount > 0) {
            for (int i = 0; i < handTransform.childCount; i++) {
                Destroy(handTransform.GetChild(i).gameObject);
            }
        }
        currentItemInHand = null;
    }
    public void PickupItem(ItemSO newItem) {
        //var tItem = Instantiate(newItem.itemPrefab, handTransform);
        currentItemInHand = Instantiate(newItem.itemPrefab, handTransform).GetComponent<Item>();

        Vector3 localPos = newItem.inHandLocalPosition;
        Vector3 localRot = newItem.inHandLocalRotation;

        currentItemInHand.transform.localPosition = localPos;
        currentItemInHand.transform.localEulerAngles = localRot;


        currentItemInHand.SetCollisionState(false);
    }
    public static ItemSO GetCurrentItemSO() {
        if (instance == null) return null;
        if (instance.currentItemInHand) {
            return instance.currentItemInHand.itemSO;
        }
        return null;
    }

    public void LossSanity(float quantity)
    {
        sanity -= quantity;
    }
}
