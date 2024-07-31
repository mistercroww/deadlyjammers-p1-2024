using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;
    public LightsController[] emergencyLightsControllers;
    public GameObject switchInterruptor;
    public AudioSource switchClip;
    public bool isGeneralBreaker = false;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager.energyOn)
        {
            switchInterruptor.transform.localEulerAngles = Vector3.right * 60;
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
            var turnOff = _gameManager.energyOn? t.TurnOn(): t.TurnOff();
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
}