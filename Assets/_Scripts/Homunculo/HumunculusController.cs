using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HumunculusController : MonoBehaviour
{
    public GameManager _gameManager;
    /* TODO: cambiar mecanica se gases, al iniciar un dia la nececidad de estos queda satisfecha y comienza a decrecer hasta que se
     *  queda totalmente vacia, por lo cual el jugador debe insertar las bombonas de gas nuevamente, lo que llenara detendra la barra de
     *  nececidad y dejara full esta
     */

    public float needsIndicatorLimit = 100;

    // Hunger Indicator
    [Range(0f, 100f)] public float hungerIndicator;
    public float _hungerDecressAmount;
    public float hungerDecreaseTimeCounter = 0.5f;
    private float _hungerDecreaseRateCounter;
    public bool hungerRefilled = false;
    public UnityEvent hungerEvent;
    public UnityEvent foodNeededEvent;

    // Oxygen Indicator
    [Range(0f, 100f)] public float oxygenIndicator;
    private float _oxygenDecressAmount;
    public float oxygenDecreaseTimeCounter = 0.5f;
    private float _oxygenDecreaseRateCounter;
    public bool oxygenRefilled = false;
    public UnityEvent oxygenSFXEvent;

    // Argon Indicator
    [Range(0f, 100f)] public float argonIndicator;
    private float _argonDecressAmount;
    public float argonDecreaseTimeCounter = 0.5f;
    private float _argonDecreaseRateCounter;
    public bool argonRefilled = false;
    public UnityEvent argonSFXEvent;


    // Nitrogen Indicator
    [Range(0f, 100f)] public float nitrogenIndicator;
    private float _nitrogenDecressAmount;
    public float nitrogenDecreaseTimeCounter = 0.5f;
    private float _nitrogenDecreaseRateCounter;
    public bool nitrogenRefilled = false;
    public UnityEvent nitrogenSFXEvent;

    // Dirty indicator
    [Range(0f, 100f)] public float dirtyIndicator;
    private float _dirtyDecressAmount;
    public float dirtyDecreaseTimeCounter = 0.5f;
    private float _dirtyDecreaseRateCounter;
    public bool dirtyRefilled = false;
    public GameObject[] organicRemains;
    public UnityEvent dirtySFXEvent;

    // Fun Indicator
    [Range(0f, 100f)] public float funIndicator;
    public float funIncreaseAmount;
    public float funIncreaseTimeCounter;
    private float _funIncreaseRateCounter;
    public float funIndicatorLimit = 10;
    public RadioController _radioController;


    // public float MadnessIndicator;
    public bool hungerKillMode = false;
    public bool gasKillMode = false;
    public float deadDecreaseTimeCounter = 20f;
    public float _deadDecreaseRateCounter = 0.1f;
    public UnityEvent startDeadEvent;
    public UnityEvent playerSanityAffect;
    public bool isDead = false;
    public UnityEvent switchKillModeEvent;

    // Needles
    public Transform hungerNeedle;
    public Transform oxygenNeedle;
    public Transform argonNeedle;
    public Transform nitrogenNeedle;
    private const float RangeRotation = 80f;

    // Assets
    public GameObject greenScreen;
    public GameObject redScreen;
    public GameObject gasGreenScreen;
    public GameObject gasRedScreen;

    // GasTank
    public GameObject oxygenGasTank;
    public GameObject argonGasTank;
    public GameObject nitrogenGasTank;
    public UnityEvent gasRefillEvent;

    // Food pieces
    public GameObject chickenPiece;
    public GameObject canPieces;

    // Animators Homuculo
    public Animator homunculoAnimator1;
    public Animator homunculoAnimator2;
    public Animator homunculoAnimator3;
    public Animator homunculoAnimator4;


    // Start is called before the first frame update
    void Start()
    {
        _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        _oxygenDecreaseRateCounter = oxygenDecreaseTimeCounter;
        _argonDecreaseRateCounter = argonDecreaseTimeCounter;
        _nitrogenDecreaseRateCounter = nitrogenDecreaseTimeCounter;
        _deadDecreaseRateCounter = deadDecreaseTimeCounter;
        UpdateNeedsMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseHunger();
        IncreaseOxygen();
        IncreaseArgon();
        IncreaseNitrogen();
        IncreaseFun();
        IncreaseDirty();

        StatsListener();
        GasListener();

        DeadCountdown();
    }

    private void setNeedleRotation(Transform needle, float Indicator)
    {
        if (needle)
        {
            needle.localRotation = Quaternion.Euler(Vector3.forward *
                                                    ExtensionMethods.Remap(Indicator, 0, 100,
                                                        RangeRotation, -RangeRotation));
        }
    }

    private void IncreaseHunger()
    {
        if (hungerRefilled)
        {
            return;
        }

        _hungerDecreaseRateCounter -= Time.deltaTime;

        if (_hungerDecreaseRateCounter < 0)
        {
            if ((hungerIndicator - _hungerDecressAmount) < needsIndicatorLimit)
            {
                hungerIndicator =
                    ExtensionMethods.AddToValueWithMax(hungerIndicator, _hungerDecressAmount, needsIndicatorLimit);

                CanPlaySound(hungerEvent, hungerIndicator, needsIndicatorLimit, 50f, false);
            }

            setNeedleRotation(hungerNeedle, hungerIndicator);
            _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        }
    }

    private void IncreaseOxygen()
    {
        if (oxygenRefilled)
        {
            return;
        }

        _oxygenDecreaseRateCounter -= Time.deltaTime;

        if (_oxygenDecreaseRateCounter < 0)
        {
            if ((oxygenIndicator - _oxygenDecressAmount) < 100)
            {
                oxygenIndicator = ExtensionMethods.AddToValueWithMax(oxygenIndicator, _oxygenDecressAmount, 100);
                CanPlaySound(oxygenSFXEvent, oxygenIndicator, needsIndicatorLimit, 50f, false);
            }

            setNeedleRotation(oxygenNeedle, oxygenIndicator);
            _oxygenDecreaseRateCounter = oxygenDecreaseTimeCounter;
        }
    }

    private void IncreaseArgon()
    {
        if (argonRefilled)
        {
            return;
        }

        _argonDecreaseRateCounter -= Time.deltaTime;

        if (_argonDecreaseRateCounter < 0)
        {
            if ((argonIndicator - _argonDecressAmount) < 100)
            {
                argonIndicator = ExtensionMethods.AddToValueWithMax(argonIndicator, _argonDecressAmount, 100);
                CanPlaySound(argonSFXEvent, argonIndicator, needsIndicatorLimit, 50f, false);
            }

            setNeedleRotation(argonNeedle, argonIndicator);
            _argonDecreaseRateCounter = argonDecreaseTimeCounter;
        }
    }

    private void IncreaseNitrogen()
    {
        if (nitrogenRefilled)
        {
            return;
        }

        _nitrogenDecreaseRateCounter -= Time.deltaTime;

        if (_nitrogenDecreaseRateCounter < 0)
        {
            if ((nitrogenIndicator - _nitrogenDecressAmount) < 100)
            {
                nitrogenIndicator = ExtensionMethods.AddToValueWithMax(nitrogenIndicator, _nitrogenDecressAmount, 100);
                CanPlaySound(nitrogenSFXEvent, nitrogenIndicator, needsIndicatorLimit, 50f, false);
            }

            setNeedleRotation(nitrogenNeedle, nitrogenIndicator);
            _nitrogenDecreaseRateCounter = nitrogenDecreaseTimeCounter;
        }
    }

    private void IncreaseDirty()
    {
        if (dirtyRefilled)
        {
            return;
        }

        _dirtyDecreaseRateCounter -= Time.deltaTime;

        if (_dirtyDecreaseRateCounter < 0)
        {
            if ((dirtyIndicator - _dirtyDecressAmount) < 100)
            {
                dirtyIndicator = ExtensionMethods.AddToValueWithMax(dirtyIndicator, _dirtyDecressAmount, 100);
                CanPlaySound(dirtySFXEvent, dirtyIndicator, needsIndicatorLimit, 50f, false);
            }

            SetDirtyLevels();
            _dirtyDecreaseRateCounter = dirtyDecreaseTimeCounter;
        }
    }

    private void CanPlaySound(UnityEvent sfxEvent, float indicator, float indicatorLimit, float shootingPercentage,
        bool touchSanity)
    {
        if (Random.Range(0, 20) == 5 && sfxEvent != null &&
            ExtensionMethods.Percentage(indicator, indicatorLimit) >= shootingPercentage)
        {
            sfxEvent.Invoke();
            if (playerSanityAffect != null && touchSanity)
            {
                playerSanityAffect.Invoke();
            }
        }
    }

    private void SetDirtyLevels()
    {
        if ((dirtyIndicator % 10) == 0)
        {
            for (int i = 0; i < organicRemains.Length; i++)
            {
                if (!organicRemains[i].activeInHierarchy)
                {
                    organicRemains[i].SetActive(true);
                    break;
                }
            }
        }
    }

    private void IncreaseFun()
    {
        _funIncreaseRateCounter -= Time.deltaTime;
        if (_funIncreaseRateCounter < 0)
        {
            if ((funIndicator - funIncreaseAmount) < funIndicatorLimit && _radioController.isOn)
            {
                funIndicator = ExtensionMethods.AddToValueWithMax(funIndicator, funIncreaseAmount, funIndicatorLimit);
            }
            else
            {
                funIndicator = ExtensionMethods.SubtractToValueWithMin(funIndicator, funIncreaseAmount, 0);
            }

            // setNeedleRotation(funNeedle, funIndicator);
            _funIncreaseRateCounter = funIncreaseTimeCounter;
        }

        // Calculate news Multiplier Needs
        UpdateNeedsMultiplier();
    }

    public void FillHunger(float foodIncome)
    {
        hungerRefilled = true;
        hungerIndicator = ExtensionMethods.SubtractToValueWithMin(hungerIndicator, foodIncome, 0);
        setNeedleRotation(hungerNeedle, hungerIndicator);
        chickenPiece.SetActive(true);
        canPieces.SetActive(true);
        foodNeededEvent?.Invoke();
    }

    public void FillOxygen(float oxygenIncome)
    {
        oxygenRefilled = true;
        oxygenIndicator = ExtensionMethods.SubtractToValueWithMin(oxygenIndicator, oxygenIncome, 0);
        setNeedleRotation(oxygenNeedle, oxygenIndicator);
        oxygenGasTank.SetActive(true);
        gasRefillEvent?.Invoke();
    }

    public void FillArgon(float argonIncome)
    {
        argonRefilled = true;
        argonIndicator = ExtensionMethods.SubtractToValueWithMin(argonIndicator, argonIncome, 0);
        setNeedleRotation(argonNeedle, argonIndicator);
        argonGasTank.SetActive(true);
        gasRefillEvent?.Invoke();
    }

    public void FillNitrogen(float nitrogenIncome)
    {
        nitrogenRefilled = true;
        nitrogenIndicator = ExtensionMethods.SubtractToValueWithMin(nitrogenIndicator, nitrogenIncome, 0);
        setNeedleRotation(nitrogenNeedle, nitrogenIndicator);
        nitrogenGasTank.SetActive(true);
        gasRefillEvent?.Invoke();
    }

    public void FillDirty(float dirtyIncome)
    {
        dirtyRefilled = true;
        dirtyIndicator = ExtensionMethods.SubtractToValueWithMin(dirtyIndicator, dirtyIncome, 0);
        SwitchOrganicRemains(false);
    }

    private void StatsListener()
    {
        if (hungerIndicator >= 100)
        {
            hungerKillMode = true;
            greenScreen.SetActive(!hungerKillMode);
            redScreen.SetActive(hungerKillMode);
        }
        else
        {
            hungerKillMode = false;
            greenScreen.SetActive(!hungerKillMode);
            redScreen.SetActive(hungerKillMode);
        }
    }

    private void GasListener()
    {
        if ((oxygenIndicator >= 100) || (argonIndicator >= 100) || (nitrogenIndicator >= 100))
        {
            gasKillMode = true;
            gasGreenScreen.SetActive(!gasKillMode);
            gasRedScreen.SetActive(gasKillMode);
        }
        else
        {
            gasKillMode = false;
            gasGreenScreen.SetActive(!gasKillMode);
            gasRedScreen.SetActive(gasKillMode);
        }
    }

    public void DeadCountdown()
    {
        if ((hungerKillMode || gasKillMode) && !isDead)
        {
            EnableDeadHomunculoAnimation();

            _deadDecreaseRateCounter -= Time.deltaTime;
            if (_deadDecreaseRateCounter < 0)
            {
                startDeadEvent?.Invoke();
                playerSanityAffect?.Invoke();
                print("has muerto pelotudo boludo papafrita");
                isDead = true;
            }
        }
        else
        {
            DisableDeadHomunculoAnimation();
            _deadDecreaseRateCounter = deadDecreaseTimeCounter;
        }
    }

    public void UpdateNeedsMultiplier()
    {
        _hungerDecressAmount = ExtensionMethods.CalculateNeedsMultiplier(_gameManager.currentDay, funIndicator,
            funIndicatorLimit, ExtensionMethods.HungerMultiplier);
        _oxygenDecressAmount = ExtensionMethods.CalculateNeedsMultiplier(_gameManager.currentDay, funIndicator,
            funIndicatorLimit, ExtensionMethods.OxygenMultiplier);
        _argonDecressAmount = ExtensionMethods.CalculateNeedsMultiplier(_gameManager.currentDay, funIndicator,
            funIndicatorLimit, ExtensionMethods.ArgonMultiplier);
        _nitrogenDecressAmount = ExtensionMethods.CalculateNeedsMultiplier(_gameManager.currentDay, funIndicator,
            funIndicatorLimit, ExtensionMethods.NitrogenMultiplier);
        _dirtyDecressAmount = ExtensionMethods.CalculateNeedsMultiplier(_gameManager.currentDay, funIndicator,
            funIndicatorLimit, ExtensionMethods.DirtyMultiplier);
    }

    public void RefreshAllNeeds()
    {
        ExtensionMethods.RandomizeArray(organicRemains);

        FillHunger(200);
        FillOxygen(200);
        FillArgon(200);
        FillNitrogen(200);
        FillDirty(-200);
        oxygenGasTank.SetActive(false);
        argonGasTank.SetActive(false);
        nitrogenGasTank.SetActive(false);
        chickenPiece.SetActive(false);
        canPieces.SetActive(false);
        SwitchOrganicRemains(true);
        hungerRefilled = false;
        oxygenRefilled = false;
        argonRefilled = false;
        nitrogenRefilled = false;
        dirtyRefilled = false;
    }

    private void SwitchOrganicRemains(bool enableIt)
    {
        for (int i = 0; i < organicRemains.Length; i++)
        {
            organicRemains[i].SetActive(enableIt);
        }
    }

    private void EnableDeadHomunculoAnimation()
    {
        switch (ExtensionMethods.CurrentAvatar(_gameManager.currentDay))
        {
            case 2:
                homunculoAnimator2.SetBool("Low", true);
                break;
        }
    }

    private void DisableDeadHomunculoAnimation()
    {
        switch (ExtensionMethods.CurrentAvatar(_gameManager.currentDay))
        {
            case 2:
                homunculoAnimator2.SetBool("Low", true);
                break;
        }
    }
}