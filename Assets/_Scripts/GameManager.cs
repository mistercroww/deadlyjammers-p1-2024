using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool energyOn = true;

    public int currentDay = 1;
    public int maxGameDay = 4;

    public GameObject _player;

    public Transform _startPoint;

    // Day parameters
    public float dayDecreaseTimeCounter = 300f;
    public float _dayDecreaseRateCounter = 0.1f;
    public UnityEvent changeDayEvent;
    public UnityEvent endGameEvent;

    private void Awake()
    {
        Application.targetFrameRate = 24;
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _dayDecreaseRateCounter = dayDecreaseTimeCounter;
    }

    private void Update()
    {
        DayTimerCountdown();
    }

    public void NextGameDay()
    {
        currentDay = (int)ExtensionMethods.AddToValueWithMax(currentDay, 1, maxGameDay);
        _dayDecreaseRateCounter = dayDecreaseTimeCounter;
        if (currentDay == maxGameDay)
        {
            if (endGameEvent != null)
            {
                endGameEvent.Invoke();
            }
        }
    }

    public void DayTimerCountdown()
    {
        _dayDecreaseRateCounter -= Time.deltaTime;
        if (_dayDecreaseRateCounter < 0)
        {
            if (changeDayEvent != null)
            {
                changeDayEvent.Invoke();
            }

            NextGameDay();
            print("Tiempo de dia finalizado, se forza el cambio de dia, dia nro " + currentDay);
            _player.transform.position = _startPoint.position;
        }
    }
}