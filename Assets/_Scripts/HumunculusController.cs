using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HumunculusController : MonoBehaviour
{
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

    // public float MadnessIndicator;
    public bool isKillMode = false;
    public float deadDecreaseTimeCounter = 1f;
    public float _deadDecreaseRateCounter = 0.1f;
    public UnityEvent startDeadEvent;
    public bool isDead = false;

    // Needles
    public Transform hungerNeedle;
    public Transform oxygenNeedle;
    public Transform cleaningNeedle;
    private float rangeRotation = 80f;


    public GameObject greenScreen;
    public GameObject redScreen;

    // Start is called before the first frame update
    void Start()
    {
        _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        _oxygenDecreaseRateCounter = oxygenDecreaseTimeCounter;
        _deadDecreaseRateCounter = deadDecreaseTimeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseHunger();
        IncreaseOxygen();

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

    public void FillHunger(float foodIncome)
    {
        hungerIndicator = ExtensionMethods.SubtractToValueWithMin(hungerIndicator, foodIncome, 0);
        setNeedleRotation(hungerNeedle, hungerIndicator);
    }

    public void FillFun(float funIncome)
    {
        oxygenIndicator = ExtensionMethods.SubtractToValueWithMin(oxygenIndicator, funIncome, 0);
        setNeedleRotation(hungerNeedle, hungerIndicator);
    }

    private void StatsListener()
    {
        if ((hungerIndicator >= 100) || (oxygenIndicator >= 100))
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