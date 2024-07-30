using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject feedChk;
    public GameObject oxygenChk;
    public GameObject argonChk;
    public GameObject nitrogenChk;

    public void CheckFeed()
    {
        feedChk.SetActive(true);
    }

    public void CheckOxygen()
    {
        oxygenChk.SetActive(true);
    }

    public void CheckArgon()
    {
        argonChk.SetActive(true);
    }

    public void CheckNitrogen()
    {
        nitrogenChk.SetActive(true);
    }

    public void ResetChecks()
    {
        feedChk.SetActive(false);
        oxygenChk.SetActive(false);
        argonChk.SetActive(false);
        nitrogenChk.SetActive(false);
    }
}