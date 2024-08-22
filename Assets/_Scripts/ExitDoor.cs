using System;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    public bool isDoorEnabled = true;

    public GameManager _gameManager;
    private ClipboardController _clipboardController;

    private void Start()
    {
        _clipboardController = FindObjectOfType<ClipboardController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadNextLevel();
        }
    }

    public bool IsInteractable()
    {
        isDoorEnabled = _clipboardController.IsAllChecked();
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