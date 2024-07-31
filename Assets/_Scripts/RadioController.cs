using System.Threading;
using UnityEngine;

public class RadioController : MonoBehaviour, IInteractable
{
    public AudioSource mixAudio;
    public AudioSource onOffAudio;
    public AudioClip[] mixes;
    public bool isOn = false;
    public GameManager _gameManager;
    public AudioClip onSFX;
    public AudioClip offSFX;

    public bool IsInteractable()
    {
        return true;
    }

    public void TriggerInteraction()
    {
        OnOffRadio();
        SetTrack();
        mixAudio.loop = true;
        if (!isOn)
        {
            print("Prendiste la radio boludo");
            mixAudio.Play();
        }
        else
        {
            print("Apagaste la radio boludo");
            mixAudio.Stop();
        }

        isOn = !isOn;
    }

    public void OnOffRadio()
    {
        onOffAudio.clip = isOn ? onSFX : offSFX;
        onOffAudio.Play();
    }

    public InteractableType InteractionType()
    {
        return InteractableType.Radio;
    }

    private void SetTrack()
    {
        mixAudio.clip = mixes[ExtensionMethods.DayTracks[_gameManager.currentDay - 1] - 1];
    }

    public void StopRadio()
    {
        mixAudio.Stop();
        isOn = false;
    }
}