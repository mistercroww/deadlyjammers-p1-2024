using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HumunculusController : MonoBehaviour
{
    public GameManager _gameManager;
    
    // Hunger Indicator
    [Range(0f, 100f)] public float hungerIndicator;
    public float hungerDecressAmount = 0.2f;
    public float hungerDecreaseTimeCounter = 0.5f;
    private float _hungerDecreaseRateCounter;

    // Oxygen Indicator
    [Range(0f, 100f)] public float oxygenIndicator;
    public float oxygenDecressAmount = 0.2f;
    public float oxygenDecreaseTimeCounter = 0.5f;
    private float _oxygenDecreaseRateCounter;

    // Argon Indicator
    [Range(0f, 100f)] public float argonIndicator;
    public float argonDecressAmount = 0.2f;
    public float argonDecreaseTimeCounter = 0.5f;
    private float _argonDecreaseRateCounter;

    // Nitrogen Indicator
    [Range(0f, 100f)] public float nitrogenIndicator;
    public float nitrogenDecressAmount = 0.2f;
    public float nitrogenDecreaseTimeCounter = 0.5f;
    private float _nitrogenDecreaseRateCounter;


    // public float MadnessIndicator;
    public bool isKillMode = false;
    public float deadDecreaseTimeCounter = 20f;
    public float _deadDecreaseRateCounter = 0.1f;
    public UnityEvent startDeadEvent;
    public bool isDead = false;

    // Needles
    public Transform hungerNeedle;
    public Transform oxygenNeedle;
    public Transform argonNeedle;
    public Transform nitrogenNeedle;
    private float rangeRotation = 80f;


    public GameObject greenScreen;
    public GameObject redScreen;

    // Start is called before the first frame update
    void Start()
    {
        _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        _oxygenDecreaseRateCounter = oxygenDecreaseTimeCounter;
        _argonDecreaseRateCounter = argonDecreaseTimeCounter;
        _nitrogenDecreaseRateCounter = nitrogenDecreaseTimeCounter;


        _deadDecreaseRateCounter = deadDecreaseTimeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        // IncreaseHunger();
        IncreaseOxygen();
        IncreaseArgon();
        IncreaseNitrogen();

        StatsListener();

        DeadCountdown();
    }

    private void setNeedleRotation(Transform needle, float Indicator)
    {
        needle.localRotation = Quaternion.Euler(Vector3.forward *
                                                ExtensionMethods.Remap(Indicator, 0, 100,
                                                    rangeRotation, -rangeRotation));
    }

    private void IncreaseHunger()
    {
        _hungerDecreaseRateCounter -= Time.deltaTime;

        if (_hungerDecreaseRateCounter < 0)
        {
            if ((hungerIndicator - hungerDecressAmount) < 100)
            {
                hungerIndicator = ExtensionMethods.AddToValueWithMax(hungerIndicator, hungerDecressAmount, 100);
            }

            setNeedleRotation(hungerNeedle, hungerIndicator);
            _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        }
    }

    private void IncreaseOxygen()
    {
        oxygenDecressAmount = ExtensionMethods.NeedsMultiplier[_gameManager.currentDay - 1];
        _oxygenDecreaseRateCounter -= Time.deltaTime;

        if (_oxygenDecreaseRateCounter < 0)
        {
            if ((oxygenIndicator - oxygenDecressAmount) < 100)
            {
                oxygenIndicator = ExtensionMethods.AddToValueWithMax(oxygenIndicator, oxygenDecressAmount, 100);
            }

            setNeedleRotation(oxygenNeedle, oxygenIndicator);
            _oxygenDecreaseRateCounter = oxygenDecreaseTimeCounter;
        }
    }

    private void IncreaseArgon()
    {
        _argonDecreaseRateCounter -= Time.deltaTime;

        if (_argonDecreaseRateCounter < 0)
        {
            if ((argonIndicator - argonDecressAmount) < 100)
            {
                argonIndicator = ExtensionMethods.AddToValueWithMax(argonIndicator, argonDecressAmount, 100);
            }

            setNeedleRotation(argonNeedle, argonIndicator);
            _argonDecreaseRateCounter = argonDecreaseTimeCounter;
        }
    }

    private void IncreaseNitrogen()
    {
        _nitrogenDecreaseRateCounter -= Time.deltaTime;

        if (_nitrogenDecreaseRateCounter < 0)
        {
            if ((nitrogenIndicator - nitrogenDecressAmount) < 100)
            {
                nitrogenIndicator = ExtensionMethods.AddToValueWithMax(nitrogenIndicator, nitrogenDecressAmount, 100);
            }

            setNeedleRotation(nitrogenNeedle, nitrogenIndicator);
            _nitrogenDecreaseRateCounter = nitrogenDecreaseTimeCounter;
        }
    }

    public void FillHunger(float foodIncome)
    {
        hungerIndicator = ExtensionMethods.SubtractToValueWithMin(hungerIndicator, foodIncome, 0);
        setNeedleRotation(hungerNeedle, hungerIndicator);
    }

    public void FillOxygen(float funIncome)
    {
        oxygenIndicator = ExtensionMethods.SubtractToValueWithMin(oxygenIndicator, funIncome, 0);
        setNeedleRotation(oxygenNeedle, oxygenIndicator);
    }

    public void FillArgon(float funIncome)
    {
        argonIndicator = ExtensionMethods.SubtractToValueWithMin(argonIndicator, funIncome, 0);
        setNeedleRotation(argonNeedle, argonIndicator);
    }

    public void FillNitrogen(float funIncome)
    {
        nitrogenIndicator = ExtensionMethods.SubtractToValueWithMin(nitrogenIndicator, funIncome, 0);
        setNeedleRotation(nitrogenNeedle, nitrogenIndicator);
    }

    private void StatsListener()
    {
        if ((hungerIndicator >= 100) || (oxygenIndicator >= 100) || (argonIndicator >= 100) ||
            (nitrogenIndicator >= 100))
        {
            isKillMode = true;
            greenScreen.SetActive(!isKillMode);
            redScreen.SetActive(isKillMode);
        }
        else
        {
            isKillMode = false;
            greenScreen.SetActive(!isKillMode);
            redScreen.SetActive(isKillMode);
        }
    }

    public void DeadCountdown()
    {
        if (isKillMode && !isDead)
        {
            _deadDecreaseRateCounter -= Time.deltaTime;
            if (_deadDecreaseRateCounter < 0)
            {
                if (startDeadEvent != null)
                {
                    startDeadEvent.Invoke();
                }

                print("has muerto pelotudo boludo papafrita");
                isDead = true;
            }
        }
        else
        {
            _deadDecreaseRateCounter = deadDecreaseTimeCounter;
        }
    }
}