using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerMovement _playerMovement;
    public bool energyOn = true;

    public int currentDay = 0;
    public int maxGameDay = ExtensionMethods.MaxDays;

    public GameObject _player;
    public HumunculusController _humunculusController;
    public Transform _startPoint;
    public GameObject gameOverScreen;

    // Day parameters
    public float newDayDecreaseTimeCounter = 10f;
    public float _newDayDecreaseRateCounter = 0.1f;
    public UnityEvent changeDayEvent;
    public UnityEvent endGameEvent;
    public TMP_Text dayTxt;
    public GameObject newDayCanvas;
    public bool newDayInit = false;

    // Contador para salir del Canvas GameOver
    [SerializeField] private float _gameOverDecreaseTimeCounter = 10f;
    private bool _gameOver = false;
    public GameObject _destroyLabTrigger;
    private void Awake()
    {
        Application.targetFrameRate = 24;
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _newDayDecreaseRateCounter = newDayDecreaseTimeCounter;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        maxGameDay = ExtensionMethods.MaxDays;
    }

    private void Update()
    {
        NewDayAnimation();
        /*
        if (Input.GetKeyDown(KeyCode.F3))
        {
            StartGameOver();
        }
        */
        if (_gameOver)
        {
            _gameOverDecreaseTimeCounter -= Time.deltaTime;
            if (_gameOverDecreaseTimeCounter < 0)
            {
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("Gameplay");
                }
            }
        }
    }

    public void NextGameDay()
    {
        currentDay = (int)ExtensionMethods.AddToValueWithMax(currentDay, 1, maxGameDay);
        _newDayDecreaseRateCounter = newDayDecreaseTimeCounter;
        if (currentDay == maxGameDay && endGameEvent != null)
        {
            endGameEvent.Invoke();
        }

        if (ExtensionMethods.CurrentAvatar(currentDay) == 3 && currentDay == 6)
        {
            _destroyLabTrigger.SetActive(true);
        }

        if (changeDayEvent != null)
        {
            changeDayEvent.Invoke();
        }

        newDayInit = true;
        _playerMovement.canDrive = false;
        newDayCanvas.SetActive(true);
        dayTxt.SetText(currentDay.ToString());
    }

    public void NewDayAnimation()
    {
        if (newDayInit)
        {
            _newDayDecreaseRateCounter -= Time.deltaTime;

            if (_newDayDecreaseRateCounter < (newDayDecreaseTimeCounter - 8))
            {
                _player.transform.SetPositionAndRotation(_startPoint.position, _startPoint.rotation);
                _playerMovement.canDrive = true;    
            }

            if (_newDayDecreaseRateCounter < 0)
            {
                newDayCanvas.SetActive(false);
                newDayInit = false;
            }
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

    public void DisableTriggerArea()
    {
        // _destroyLabTrigger.SetActive(false);
    }
}