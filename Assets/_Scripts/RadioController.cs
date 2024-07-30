using UnityEngine;

public class RadioController : MonoBehaviour, IInteractable
{
    public AudioSource mixAudio;
    public AudioClip[] mixes;
    public float volume = 0.33f;
    public bool isOn = false;
    public GameManager _gameManager;

    public bool IsInteractable()
    {
        return true;
    }

    public void TriggerInteraction()
    {
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

    public InteractableType InteractionType()
    {
        return InteractableType.Radio;
    }

    private void SetTrack()
    {
        mixAudio.clip = mixes[ExtensionMethods.DayTracks[_gameManager.currentDay - 1]];
    }
}