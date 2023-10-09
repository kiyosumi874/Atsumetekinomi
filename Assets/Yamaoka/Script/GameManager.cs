using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    BeforeGame,
    InGame,
    AfterGame
}

public class GameManager : MonoBehaviour
{
    public int countDownMinutes;
    public float countDownSeconds;
    public float gameStartCountDownSeconds;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text countDownText;
    public bool finishCountDown = false;

    [SerializeField]
    GameState gameState = GameState.BeforeGame;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //countDownSeconds = countDownMinutes * 60;
        timeText.enabled = false;
        countDownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CountDown(countDownSeconds);

        switch(gameState)
        {
            case GameState.BeforeGame:
                score = 0;
                GameStartCountDown();
                break;
            case GameState.InGame:
                CountDown();
                break;
            case GameState.AfterGame:
                //score = ComboManager.instance.score;
                score = ResultScore();
                break;
        }
    }

    public void ChangeGameState(GameState nextGameState)
    {
        gameState = nextGameState;
    }

    public void CountDown()
    {
        timeText.enabled = true;
        countDownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countDownSeconds);
        timeText.text = span.ToString(@"mm\:ss");

        if(countDownSeconds <= 0)
        {
            //finishCountDown = true;
            countDownSeconds = 0;
            ChangeGameState(GameState.AfterGame);
        }
    }

    public void GameStartCountDown()
    {
        countDownText.enabled = true;
        gameStartCountDownSeconds -= Time.deltaTime;
        //var span = new TimeSpan(0, 0, (int)gameStartCountDownSeconds);
        countDownText.text = ((int)gameStartCountDownSeconds).ToString();

        if (gameStartCountDownSeconds <= 0)
        {
            countDownText.enabled = false;
            gameStartCountDownSeconds = 0;
            //finishCountDown = true;
            ChangeGameState(GameState.InGame);
        }
    }

    public int ResultScore()
    {
        score = ComboManager.instance.score;
        return score;
    }
}
