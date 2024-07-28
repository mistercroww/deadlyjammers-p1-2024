using System;
using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    public bool IsInteractable()
    {
        return true;
    }

    public void TriggerInteraction()
    {
        for (int i = 0; i < lightsControllers.Length; i++)
        {
            lightsControllers[i].SwitchLights();
        }
    }


    public void ToggleEnergy()
    {
        
    }
    public InteractableType InteractionType() {
        return InteractableType.Switch;
    }
}