using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Homunculo
{
    public class HomSFXController : MonoBehaviour
    {
        private GameManager _gameManager;
        public AudioSource mixAudio;
        public AudioSource beepAudio;
        public AudioClip beepSFX;
        public AudioClip[] eatSFX;
        public HumunculusController _humunculusController;

        public float beepIncreaseTimeCounter;
        public float speedyBeepIncreaseTimeCounter;
        public float _beepIncreaseRateCounter;


        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            ComputerBeep();
        }

        public void Hungry()
        {
            mixAudio.clip = eatSFX[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)];
            mixAudio.Play();
        }

        private void ComputerBeep()
        {
            _beepIncreaseRateCounter -= Time.deltaTime;
            if (_beepIncreaseRateCounter < 0)
            {
                beepAudio.clip = beepSFX;
                if (_humunculusController.isKillMode)
                {
                    beepAudio.Play();
                    _beepIncreaseRateCounter = speedyBeepIncreaseTimeCounter;
                }
                else
                {
                    beepAudio.Play();
                    _beepIncreaseRateCounter = beepIncreaseTimeCounter;
                }
            }
        }
    }
}