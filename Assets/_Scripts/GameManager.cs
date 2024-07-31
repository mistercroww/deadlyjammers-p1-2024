using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool energyOn = true;

    public int currentDay = 0;
    public int maxGameDay = ExtensionMethods.MaxDays;

    public GameObject _player;
    public HumunculusController _humunculusController;
    public Transform _startPoint;
    public GameObject gameOverScreen;
    
    // Day parameters
    public float dayDecreaseTimeCounter = 300f;
    public float _dayDecreaseRateCounter = 0.1f;
    public UnityEvent changeDayEvent;
    public UnityEvent endGameEvent;

    // Contador para salir del Canvas GameOver
    [SerializeField]private float _gameOverDecreaseTimeCounter = 10f;
    private bool _gameOver = false;

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
        maxGameDay = ExtensionMethods.MaxDays;
    }

    private void Update()
    {
        // DayTimerCountdown();
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            StartGameOver();
        }

        if (_gameOver)
        {
            _gameOverDecreaseTimeCounter -= Time.deltaTime;
            if (_gameOverDecreaseTimeCounter < 0)
            {
                if (Input.anyKey)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
                }
            }
        }
    }

    public void NextGameDay()
    {
        currentDay = (int)ExtensionMethods.AddToValueWithMax(currentDay, 1, maxGameDay);
        _dayDecreaseRateCounter = dayDecreaseTimeCounter;
        if (currentDay == maxGameDay && endGameEvent != null)
        {
            endGameEvent.Invoke();
        }

        if (changeDayEvent != null)
        {
            changeDayEvent.Invoke();
        }
    }

    public void DayTimerCountdown()
    {
        _dayDecreaseRateCounter -= Time.deltaTime;
        if (_dayDecreaseRateCounter < 0)
        {
            _humunculusController?.startDeadEvent?.Invoke();

            NextGameDay();
            print("Tiempo de dia finalizado, se forza el cambio de dia, dia nro " + currentDay);
            _player.transform.position = _startPoint.position;
        }
    }

    public void KillPlayer()
    {
        changeDayEvent?.Invoke();
        TeleportPlayerToStartPoint();
    }

    public void TeleportPlayerToStartPoint()
    {
        _player.transform.position = _startPoint.position;
    }

    public void StartGameOver()
    {
        gameOverScreen.SetActive(true);
        _gameOver = true;
    }
}