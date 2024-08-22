using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class FinalHomunculoController : MonoBehaviour
{
    private GameManager _gameManager;
    public PlayableDirector playableDirector;
    private PlayerMovement playerMovement;
    public GameObject clipboard;
    public GameObject interactionText;

    public AudioSource audioMix;
    public AudioClip feedSfx;
    public AudioClip idleHomun;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void feedHomunculo()
    {
        if (_gameManager.currentDay == ExtensionMethods.MaxDays)
        {
            LastFood();
        }
        else
        {
            audioMix.clip = feedSfx;
            audioMix.Play();
        }
        
    }
    
    public void LastFood()
    {
        playableDirector.Play();
        playerMovement.canDrive = false;
        clipboard.SetActive(false);
        interactionText.SetActive(false);
    }
    
    
    
}
