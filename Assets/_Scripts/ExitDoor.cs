using System;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    public bool isDoorEnabled = true;

    public GameManager _gameManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadNextLevel();
        }
    }

    public bool IsInteractable()
    {
        return isDoorEnabled;
    }

    public InteractableType InteractionType()
    {
        return InteractableType.Door;
    }

    public void TriggerInteraction()
    {
        if (!isDoorEnabled) return;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        _gameManager.NextGameDay();
        print("PASASTE EL DIA MACACO, dia " + _gameManager.currentDay);
    }
}