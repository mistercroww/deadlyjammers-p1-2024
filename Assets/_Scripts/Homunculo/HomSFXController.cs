using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Scripts.Homunculo
{
    public class HomSFXController : MonoBehaviour
    {
        private GameManager _gameManager;
        public AudioSource mixAudio;
        public AudioSource beepAudio;
        public AudioClip beepSFX;
        public AudioClip[] eatSfx;
        public AudioClip[] hungerSfx;
        public AudioClip[] helpSfx;
        public AudioClip[] gasReleaseSfx;
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

        public void runFoodSfx(int hungerType)
        {
            switch (hungerType)
            {
                case 1:
                    // Sonido de necesidad de comida
                    mixAudio.clip = hungerSfx[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)];
                    break;
                case 2:
                    // Sonido de comer
                    mixAudio.clip = eatSfx[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)];
                    break;
            }

            mixAudio.Play();
        }

        public void runHelpSfx(int helpType)
        {
            switch (helpType)
            {
                case 1:
                    // Sonido de necesidad de ayuda (cualquier gas y limpiesa)
                    mixAudio.clip = helpSfx[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)];
                    break;
            }

            mixAudio.Play();
        }

        public void runGasSfx(int helpType)
        {
            switch (helpType)
            {
                case 1:
                    // Sonido de necesidad gas al recargarlos
                    mixAudio.clip = gasReleaseSfx[Random.Range(0, 1)];
                    break;
                case 2:
                    // Sonido de necesidad de ayuda (cualquier gas y limpiesa)
                    mixAudio.clip = helpSfx[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)];
                    break;
            }

            mixAudio.Play();
        }

        private void ComputerBeep()
        {
            _beepIncreaseRateCounter -= Time.deltaTime;
            if (_beepIncreaseRateCounter < 0)
            {
                beepAudio.clip = beepSFX;
                if (_humunculusController.hungerKillMode)
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