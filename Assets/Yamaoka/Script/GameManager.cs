using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームの状態を設定
/// </summary>
public enum GameState
{
    BeforeGame,     // ゲーム開始前
    InGame,         // ゲーム中
    AfterGame       // ゲーム終了
}

public class GameManager : MonoBehaviour
{
    // ゲーム開始までのカウントダウン用
    public float gameStartCountDownSeconds;
    [SerializeField]
    private Text countDownText;
    // ゲーム中の制限時間設定用
    public float countDownSeconds;
    [SerializeField]
    private Text timeText;

    //public bool finishCountDown = false;    // カウントダウンが終了したかどうか

    [SerializeField]
    GameState gameState = GameState.BeforeGame;

    public int score = 0;   // 最終スコア

    // Start is called before the first frame update
    void Start()
    {
        //timeText.enabled = false;
        countDownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.BeforeGame:
                // スコアを初期化する
                score = 0;
                GameStartCountDown();
                break;
            case GameState.InGame:
                CountDown();
                break;
            case GameState.AfterGame:
                score = ResultScore();
                break;
        }
    }

    /// <summary>
    /// ゲームステータスの変更を行う
    /// </summary>
    /// <param name="nextGameState">次のゲームステータス</param>
    public void ChangeGameState(GameState nextGameState)
    {
        gameState = nextGameState;
    }

    /// <summary>
    /// ゲーム中の時間計測を行う
    /// </summary>
    public void CountDown()
    {
        //timeText.enabled = true;
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

    /// <summary>
    /// ゲーム開始前のカウントダウン処理
    /// </summary>
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

    /// <summary>
    /// スコアの受け渡しを行う
    /// </summary>
    /// <returns>ゲーム中に獲得したスコア</returns>
    public int ResultScore()
    {
        score = ComboManager.instance.score;
        return score;
    }
}
