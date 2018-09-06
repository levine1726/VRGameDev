using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    public static int currentScore = 0;

    [SerializeField]
    private GameObject timer;

    [SerializeField]
    private GameObject startGameNotification;
    private static TextMesh timeOnBoard;

    [SerializeField]
    private GameObject scoreObject;
    private static TextMesh scoreText;

    [SerializeField]
    private GameObject startGameTextObject;
    private static TextMesh startGameText;

    [SerializeField]
    private GameObject endGameTextObject;
    private static TextMesh endGameText;

    [SerializeField]
    private GameObject basketballPrefabs;
    
    private GameObject rackAlias;

    [SerializeField]
    private GameObject countdownTextObject;
    private static TextMesh countdownText;

    private static float countdownTimer;

    public static float gameTimer;
    public static bool gameStarted;
    public static bool gameEnded;
    public static bool countDownStarted;
    public static bool lockScoreboard;
    
    

    private void Awake()
    {
        timeOnBoard = timer.GetComponent<TextMesh>();
        startGameText = startGameTextObject.GetComponent<TextMesh>();
        countdownText = countdownTextObject.GetComponent<TextMesh>();
        scoreText = scoreObject.GetComponent<TextMesh>();
        endGameText = endGameTextObject.GetComponent<TextMesh>();
        rackAlias = Instantiate<GameObject>(basketballPrefabs);


        currentScore = 0;
        countdownTimer = 3f;
        gameTimer = 30f;
        gameStarted = false;
        gameEnded = false;
        countDownStarted = false;
        lockScoreboard = false;
    }

    private void FixedUpdate()
    {
        if (gameEnded)
        {
            EndGame();
        }
        else if (countDownStarted)
        {
            countdownText.gameObject.SetActive(true);
            StartGameCountdown(countdownText);
        }
        else if (gameStarted) {
            scoreText.text = "Score: " + currentScore;
            UpdateTimer();
        }
    }

    public static void IncrementScore()
    {
        currentScore++;
        Debug.Log("Current Score is " + currentScore);

    }

    private void EndGame()
    {
        lockScoreboard = false;
        gameTimer = 30f;
        countdownTimer = 3f;
        endGameText.gameObject.SetActive(true);
        gameEnded = false;
        countDownStarted = false;
        gameStarted = false;
        Destroy(rackAlias);
        rackAlias = Instantiate<GameObject>(basketballPrefabs);
    }

    private static void UpdateTimer()
    {
        if (0 < gameTimer)
        {
            gameTimer -= Time.deltaTime;
          
            timeOnBoard.text = "Timer: " + gameTimer.ToString("n1");
        } else
        {
            gameEnded = true;
            timeOnBoard.text = "Timer: 0.0";
        }
    }

    public static void StartGame()
    {
        countDownStarted = true;
        lockScoreboard = true;
        endGameText.gameObject.SetActive(false); // in case game is replayed when over
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
        Debug.Log("game started");
        startGameText.gameObject.SetActive(false);
        
    }

    private static void StartGameCountdown(TextMesh countText)
    {

        countdownTimer -= Time.deltaTime;
        if (1f < countdownTimer && countdownTimer < 2f)
        {
            countText.text = "2";
        }
        else if (0f < countdownTimer && countdownTimer < 1f)
        {
            countText.text = "1";
        }
        else if (countdownTimer <= 0f)
        {
            countText.gameObject.SetActive(false);
            countText.text = "3"; // reset for future use
            countDownStarted = false;
            gameStarted = true;
        }



    }


}
