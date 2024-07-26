using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumunculusController : MonoBehaviour
{

    public float DecressAmount = 0.2f;
    public float DecreaseTimeCounter = 0.5f;
    public float HungerIndicator;
    private float _hungerDecreaseRateCounter;
    
    public float MadnessIndicator;
    public float CleaningIndicator;
    public float FunIndicator;
    public float HealthIndicator;

    public bool IsKillMode;


    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _hungerDecreaseRateCounter = DecreaseTimeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void IncreaseHunger()
    {
        _hungerDecreaseRateCounter -= Time.deltaTime;

        if (_hungerDecreaseRateCounter < 0)
        {
            if ((HungerIndicator - DecressAmount) < 100)
            {
                HungerIndicator += DecressAmount;
            }

            // staminaUpdate?.Invoke();
            // _hungerDecreaseRateCounter = staminaGenRate;
        }
    }
}
