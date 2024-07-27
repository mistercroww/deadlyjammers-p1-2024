using UnityEngine;
using UnityEngine.Serialization;

public class HumunculusController : MonoBehaviour
{
    // Hunger Indicator
    public float hungerIndicator;
    public float hungerDecressAmount = 0.2f;
    public float hungerDecreaseTimeCounter = 0.5f;
    private float _hungerDecreaseRateCounter;

    // Fun Indicator
    public float funIndicator;
    public float funDecressAmount = 0.2f;
    public float funDecreaseTimeCounter = 0.5f;
    private float _funDecreaseRateCounter;


    // CleaningIndicator
    public float cleaningIndicator;
    public float cleaningDecressAmount = 0.2f;
    public float cleaningDecreaseTimeCounter = 0.5f;
    private float _cleaningDecreaseRateCounter;

    // public float MadnessIndicator;


    public bool IsKillMode;

    // Needles
    public Transform hungerNeedle;
    public Transform funNeedle;
    public Transform cleaningNeedle;
    private float rangeRotation = 60f;

    // Start is called before the first frame update
    void Start()
    {
        _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        _funDecreaseRateCounter = funDecreaseTimeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseHunger();
        // IncreaseFun();
        // IncreaseDirty
    }

    private void setNeedleRotation(Transform needle, float Indicator)
    {
        needle.localRotation = Quaternion.Euler(Vector3.forward *
                                                ExtensionMethods.Remap(Indicator, 0, 100,
                                                    -rangeRotation, rangeRotation));
    }

    private void IncreaseHunger()
    {
        _hungerDecreaseRateCounter -= Time.deltaTime;

        if (_hungerDecreaseRateCounter < 0)
        {
            if ((hungerIndicator - hungerDecressAmount) < 100)
            {
                hungerIndicator += hungerDecressAmount;
            }

            setNeedleRotation(hungerNeedle, hungerIndicator);
            _hungerDecreaseRateCounter = hungerDecreaseTimeCounter;
        }
    }

    private void IncreaseFun()
    {
        _funDecreaseRateCounter -= Time.deltaTime;

        if (_funDecreaseRateCounter < 0)
        {
            if ((funIndicator - funDecressAmount) < 100)
            {
                funIndicator += funDecressAmount;
            }

            setNeedleRotation(funNeedle, funIndicator);
            _funDecreaseRateCounter = funDecreaseTimeCounter;
        }
    }

    private void IncreaseDirty()
    {
        _cleaningDecreaseRateCounter -= Time.deltaTime;

        if (_cleaningDecreaseRateCounter < 0)
        {
            if ((cleaningIndicator - cleaningDecressAmount) < 100)
            {
                cleaningIndicator += cleaningDecressAmount;
            }

            setNeedleRotation(cleaningNeedle, cleaningIndicator);
            _cleaningDecreaseRateCounter = cleaningDecreaseTimeCounter;
        }
    }
}