using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public bool hungerFilled;
    public bool oxygenFilled;
    public bool argonFilled;
    public bool nitrogenFilled;
    public bool dirtyFilled;

    public GameObject hungerChecked;
    public GameObject hungerUnchecked;
    public GameObject gasChecked;
    public GameObject gasUnchecked;
    public GameObject cleanChecked;
    public GameObject cleanUnchecked;

    public void CheckFeed()
    {
        hungerFilled = true;
        HungerCheck();
    }

    public void CheckOxygen()
    {
        oxygenFilled = true;
        GasCheck();
    }

    public void CheckArgon()
    {
        argonFilled = true;
        GasCheck();
    }

    public void CheckNitrogen()
    {
        nitrogenFilled = true;
        GasCheck();
    }

    public void CheckDirty()
    {
        dirtyFilled = true;
        DirtyCheck();
    }

    public void ResetChecks()
    {
        hungerFilled = false;
        oxygenFilled = false;
        argonFilled = false;
        nitrogenFilled = false;
        dirtyFilled = false;

        HungerCheck();
        GasCheck();
        DirtyCheck();
    }

    private void HungerCheck()
    {
        if (hungerFilled)
        {
            hungerChecked.SetActive(true);
            hungerUnchecked.SetActive(false);
        }
        else
        {
            hungerChecked.SetActive(false);
            hungerUnchecked.SetActive(true);
        }
    }

    private void GasCheck()
    {
        if (oxygenFilled && argonFilled && nitrogenFilled)
        {
            gasChecked.SetActive(true);
            gasUnchecked.SetActive(false);
        }
        else
        {
            gasChecked.SetActive(false);
            gasUnchecked.SetActive(true);
        }
    }
    private void DirtyCheck()
    {
        if (dirtyFilled)
        {
            cleanChecked.SetActive(true);
            cleanUnchecked.SetActive(false);
        }
        else
        {
            cleanChecked.SetActive(false);
            cleanUnchecked.SetActive(true);
        }
    }
}