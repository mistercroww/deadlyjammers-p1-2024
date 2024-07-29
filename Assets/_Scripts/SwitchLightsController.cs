using UnityEngine;

public class SwitchLights : MonoBehaviour, IInteractable
{
    public LightsController[] lightsControllers;
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

        for (int i = 0; i < lightsControllers.Length; i++)
        {
            isOn = lightsControllers[i].SwitchLights();
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
        bool isOn = false;

        for (int i = 0; i < lightsControllers.Length; i++)
        {
            isOn = isGeneralBreaker ? lightsControllers[i].TurnOff() : lightsControllers[i].SwitchLights();
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

        if (isGeneralBreaker)
        {
            _gameManager.energyOn = !_gameManager.energyOn;
        }
    }

    public InteractableType InteractionType()
    {
        return InteractableType.Switch;
    }
}