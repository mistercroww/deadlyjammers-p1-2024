using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;
    public LightsController[] emergencyLightsControllers;
    public GameObject switchInterruptor;
    public AudioSource switchClip;
    public AudioSource breakerDownSfx;
    public bool isGeneralBreaker = false;

    // Timer para ver si se corta la luz
    private float _powerOffDecressAmount = 0.1f;
    private float _powerOffDecreaseTimeCounter = 5f;
    private float _powerOffDecreaseRateCounter;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager.energyOn)
        {
            switchInterruptor.transform.localEulerAngles = Vector3.right * 60;
        }

        _powerOffDecreaseRateCounter = _powerOffDecreaseTimeCounter;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F2) && isGeneralBreaker)
        {
            TogglePower();
        }
        */
        if (isGeneralBreaker)
        {
            PowerOffTimer();
        }
    }

    public bool IsInteractable()
    {
        return isGeneralBreaker ? true : _gameManager.energyOn;
    }

    public void TriggerInteraction()
    {
        if (isGeneralBreaker)
        {
            BreakerSwitch();
        }
        else
        {
            GeneralTrigger();
        }
    }


    public void GeneralTrigger()
    {
        bool isOn = false;
        foreach (var t in lightsControllers)
        {
            isOn = t.SwitchLights();
        }

        if (switchInterruptor)
        {
            if (!isOn)
            {
                switchInterruptor.transform.localEulerAngles = -(Vector3.right * 60);
            }
            else
            {
                switchInterruptor.transform.localEulerAngles = (Vector3.right * 60);
            }
        }

        switchClip.Play();
    }

    public void BreakerSwitch()
    {
        if (isGeneralBreaker)
        {
            _gameManager.energyOn = !_gameManager.energyOn;
        }

        foreach (var t in lightsControllers)
        {
            var turnOff = _gameManager.energyOn ? t.TurnOn() : t.TurnOff();
        }

        foreach (var t in emergencyLightsControllers)
        {
            t.SwitchLights();
        }

        if (switchInterruptor)
        {
            if (!_gameManager.energyOn)
            {
                switchInterruptor.transform.localEulerAngles = -(Vector3.right * 60);
            }
            else
            {
                switchInterruptor.transform.localEulerAngles = (Vector3.right * 60);
            }
        }

        switchClip.Play();
    }

    public InteractableType InteractionType()
    {
        return InteractableType.Switch;
    }

    public void TogglePower()
    {
        if (_gameManager.energyOn)
        {
            BreakerSwitch();
            if (breakerDownSfx)
            {
                breakerDownSfx.Play();
            }
        }
        else
        {
            BreakerSwitch();
        }
    }

    public void PowerOff()
    {
        if (isGeneralBreaker)
        {
            _gameManager.energyOn = false;
        }

        foreach (var t in lightsControllers)
        {
            var turnOff = _gameManager.energyOn ? t.TurnOn() : t.TurnOff();
        }

        foreach (var t in emergencyLightsControllers)
        {
            t.SwitchLights();
        }

        if (switchInterruptor)
        {
            if (!_gameManager.energyOn)
            {
                switchInterruptor.transform.localEulerAngles = -(Vector3.right * 60);
            }
            else
            {
                switchInterruptor.transform.localEulerAngles = (Vector3.right * 60);
            }
        }

        switchClip.Play();
    }

    private void PowerOffTimer()
    {
        if (_gameManager.energyOn)
        {
            _powerOffDecreaseRateCounter -= Time.deltaTime;

            if (_powerOffDecreaseRateCounter < 0)
            {
                if (Random.value <= 0.1f)
                {
                    TogglePower();
                }

                _powerOffDecreaseRateCounter = _powerOffDecreaseTimeCounter;
            }
        }
    }
}