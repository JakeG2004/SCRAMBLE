using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Vector3 _stats = Vector3.zero;
    [SerializeField] private string _nextLevel = "Level1";
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _summaryText;
    [SerializeField] private TMP_Text _nextButtonText;
    [SerializeField] private GameObject _summary;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private int _scoreToBeat = 0;

    [SerializeField] private float _time = 120f;
    private bool _levelIsStarted = false;
    private bool _levelIsComplete = false;
    private bool _gameIsPaused = false;

    void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }

        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _summary.SetActive(false);
        _pauseScreen.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }

        if(!_levelIsStarted || _levelIsComplete)
        {
            return;
        }

        _time -= Time.deltaTime;
        UpdateTimeText();

        if(_time <= 0)
        {
            EndLevel();
        }
    }

    public void SetLevelTime(float newTime)
    {
        _time = newTime;
    }

    public void SetRequiredAmt(int amt)
    {
        _scoreToBeat = amt;
    }

    public void PauseScreen()
    {
        if(_gameIsPaused)
        {
            Time.timeScale = 1f;
            _pauseScreen.SetActive(false);
            _gameIsPaused = false;
        }

        else
        {
            Time.timeScale = 0f;
            _pauseScreen.SetActive(true);
            _gameIsPaused = true;
        }
    }

    public void StartLevel()
    {
        _levelIsStarted = true;
        Time.timeScale = 1f;
    }

    private void UpdateTimeText()
    {
        if(_timerText == null)
        {
            Debug.Log("Failed to get timer text");
            return;
        }

        if(_time <= 0)
        {
            _timerText.text = "0:00";
            return;
        }

        int minutes = (int)Mathf.Floor(_time / 60);
        int seconds = (int)Mathf.Floor(_time - (minutes * 60));

        _timerText.text = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);
    }

    public void EndLevel()
    {
        Time.timeScale = 0f;
        _levelIsComplete = true;
        ShowSummary();
    }

    public void ShowSummary()
    {
        string summaryText = "Score: " + _stats.x + "\nSuccesses: " + _stats.y + "\nFailures: " + _stats.z;
        if(_stats.x >= _scoreToBeat)
        {
            summaryText += "\nCongrats. You keep your job!";
            _nextButtonText.text = "Next";
        }

        else 
        {
            summaryText += "\nYou're fired!";
            _nextButtonText.text = "Retry";
        }

        _summaryText.text = summaryText;

        _summary.SetActive(true);
    }

    public void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNext()
    {
        if(_stats.x >= _scoreToBeat)
        {
            SceneManager.LoadScene(_nextLevel);
        }

        else
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ServeCorrect()
    {
        IncrementScore();
        _stats.y++;
    }

    public void ServeIncorrect()
    {
        DecrementScore();
        _stats.z++;
    }

    private void IncrementScore()
    {
        _stats.x++;
    }

    private void DecrementScore()
    {
        _stats.x--;
    }

    public Vector3 GetScore()
    {
        return _stats;
    }
}
