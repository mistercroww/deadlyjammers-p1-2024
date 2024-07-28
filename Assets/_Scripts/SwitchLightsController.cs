using System;
using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;
    public GameObject switchInterruptor;

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
        bool isOn = false;
        for (int i = 0; i < lightsControllers.Length; i++)
        {
            isOn = lightsControllers[i].SwitchLights();
        }

        if (switchInterruptor)
        {
            if (!isOn)
            {
                switchInterruptor.transform.localEulerAngles = Vector3.right * 60;
            }
            else
            {
                switchInterruptor.transform.localEulerAngles = -(Vector3.right * 60);
            }
        }
    }


    public void ToggleEnergy()
    {
    }
}