using System;
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

    public GameObject clipboard1;
    public GameObject clipboard2;
    
    public GameObject hungerChecked2;
    public GameObject hungerUnchecked2;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void CheckFeed()
    {
        hungerFilled = true;
        HungerCheck();
    }
    
    public void CheckFeed2()
    {
        hungerFilled = true;
        HungerCheck2();
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
        HungerCheck2();
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
    
    public void HungerCheck2()
    {
        if (hungerFilled)
        {
            hungerChecked2.SetActive(true);
            hungerUnchecked2.SetActive(false);
        }
        else
        {
            hungerChecked2.SetActive(false);
            hungerUnchecked2.SetActive(true);
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

    public void ToggleClipboard()
    {
        clipboard1.SetActive(false);
        clipboard2.SetActive(true);
    }
    

    public bool IsAllChecked()
    {
        if (ExtensionMethods.CurrentAvatar(_gameManager.currentDay) >= 3 && _gameManager.currentDay >= 6)
        {
            return hungerFilled;
        }
        else
        {
            return hungerFilled && oxygenFilled && argonFilled && nitrogenFilled && dirtyFilled;
        }
    }
}