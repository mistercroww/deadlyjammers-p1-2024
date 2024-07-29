using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public bool IsOn = true;

    public bool SwitchLights()
    {
        IsOn = !IsOn;
        on.SetActive(IsOn);
        off.SetActive(!IsOn);
        return IsOn;
    }

    public bool TurnOff()
    {
        on.SetActive(false);
        off.SetActive(true);
        return false;
    }
}