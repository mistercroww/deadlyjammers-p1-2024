using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemReceiver : MonoBehaviour, IInteractable
{
    public bool isInteractable = true;
    public ItemSO itemToReceiveSO;
    public bool removeItemFromPlayerWhenUsed = true;
    public UnityEvent OnCorrectItemReceived;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public InteractableType InteractionType()
    {
        return InteractableType.ItemReceiver;
    }

    public bool IsInteractable()
    {
        return _playerController.currentItemInHand != null && isInteractable;
    }

    public void TriggerInteraction()
    {
        if (!isInteractable) return;
        var playerItem = PlayerController.GetCurrentItemSO();
        if (playerItem != null && playerItem.itemID == itemToReceiveSO.itemID && OnCorrectItemReceived != null)
        {
            OnCorrectItemReceived.Invoke();
            if (removeItemFromPlayerWhenUsed)
            {
                PlayerController.instance.ClearCurrentItem(false);
            }
        }
    }
}