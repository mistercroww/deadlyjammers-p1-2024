using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;

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
}