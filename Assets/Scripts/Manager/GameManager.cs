using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


namespace Assignment.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Fields
        public static GameManager Instance;
        [SerializeField] float totalTime = 60f;
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] GameObject gameLoseScreen;
        [SerializeField] GameObject gameWonScreen;
        [SerializeField] TextMeshProUGUI coinsValue;

        [HideInInspector]
        public bool gameWinCondition;

        private float _timeRemaining;
        private bool _isTimerRunning = false;

        //Action Delegates for Game Over conditions
        public Action GameWin;
        public Action GameLose;

        #endregion

        #region Monobehaviours
        private void OnEnable()
        {
            GameWin += GameWon;
            GameLose += GameLost;
        }
        private void OnDisable()
        {
            GameWin -= GameWon;
            GameLose -= GameLost;
        }

        private void Awake()
        {
            //Singelton Pattern
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else {
                Instance = this;
            }
            
        }
        private void Start()
        {
            _timeRemaining = totalTime;
            UpdateTimerDisplay();
            StartTimer();
        }

        #endregion

        #region Methods
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void UpdateCoins(int value)
        {
            int currentCoinsCount = int.Parse(coinsValue.text);
            currentCoinsCount += value;
            coinsValue.text = currentCoinsCount.ToString();
            if (int.Parse(coinsValue.text) >= 20)
            {
                GameManager.Instance.GameWin?.Invoke();
                GameManager.Instance.gameWinCondition = true;
            }
        }
        private void Update()
        {
            if (_isTimerRunning)
            {
                if (_timeRemaining > 0f)
                {
                    _timeRemaining -= Time.deltaTime;
                    UpdateTimerDisplay();
                }
                else
                {
                   
                   
                        _isTimerRunning = false;
                    if (!GameManager.Instance.gameWinCondition)
                        GameManager.Instance.GameLose?.Invoke();


                }
            }
        }

        public void StartTimer()
        {
            _isTimerRunning = true;
        }

        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timerString;
        }


        public void GameWon() {
            gameWonScreen.SetActive(true);
        }
        public void GameLost() { 
            gameLoseScreen.SetActive(true);
        }
        #endregion
    }
}