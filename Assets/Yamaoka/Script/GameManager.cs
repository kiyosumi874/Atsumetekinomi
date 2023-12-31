using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ゲームの状態を設定
/// </summary>
public enum GameState
{
    BeforeGame,     // ゲーム開始前
    InGame,         // ゲーム中
    AfterGame,      // ゲーム終了
    None,
}

public class GameManager : MonoBehaviour
{
    // ゲーム開始までのカウントダウン用
    public float gameStartCountDownSeconds;
    // ゲーム中の制限時間設定用
    public float countDownSeconds;

    // 各時間の初期値を保存する変数
    public float startCountDownTime;
    public float gameTime;

    public GameState gameState = GameState.BeforeGame;

    public int score = 0;   // 最終スコア

    public static GameManager instance;      // インスタンス


    private void Awake()
    {
        CheckInstance();
        //gameState = GameState.BeforeGame;
        // 各時間の初期値を設定
        startCountDownTime = gameStartCountDownSeconds;
        gameTime = countDownSeconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        //countDownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleTest")
        {
            ChangeGameState(GameState.None);
        }

        switch (gameState)
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
                SceneChanger.Instance.LoadSceneFaded("Ranking");
                break;
            case GameState.None:
                // 各時間を初期化する
                gameStartCountDownSeconds = startCountDownTime;
                countDownSeconds = gameTime;
                // スコアを初期化する
                score = 0;
                // 現在のシーンがゲームシーンの時
                if (SceneManager.GetActiveScene().name == "TestGameScene")
                {
                    ChangeGameState(GameState.BeforeGame);
                }
                break;
        }
    }

    /// <summary>
    /// ゲームシーンからリザルトシーンに遷移する際に呼ぶ
    /// </summary>
    public void ChangeGameToResult()
    {
        ChangeGameState(GameState.AfterGame);
        //gameStartCountDownSeconds = startCountDownTime;
        //countDownSeconds = gameTime;
    }

    /// <summary>
    /// ゲームシーンからタイトルシーンに遷移する際に呼ぶ
    /// </summary>
    public void ChangeGameToTitle()
    {
        ChangeGameState(GameState.None);
        //gameStartCountDownSeconds = startCountDownTime;
        //countDownSeconds = gameTime;
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
        //timeText.text = span.ToString(@"mm\:ss");
        UIManager.instance.gameTimeText.text = span.ToString(@"mm\:ss");

        if (countDownSeconds <= 0)
        {
            countDownSeconds = gameTime;
            ChangeGameState(GameState.AfterGame);
        }
    }

    /// <summary>
    /// ゲーム開始前のカウントダウン処理
    /// </summary>
    public void GameStartCountDown()
    {
        //countDownText.enabled = true;
        UIManager.instance.countDownText.enabled = true;
        gameStartCountDownSeconds -= Time.deltaTime;
        //var span = new TimeSpan(0, 0, (int)gameStartCountDownSeconds);
        //countDownText.text = ((int)gameStartCountDownSeconds).ToString();
        UIManager.instance.countDownText.text = ((int)gameStartCountDownSeconds).ToString();

        if (gameStartCountDownSeconds <= 0)
        {
            UIManager.instance.countDownText.enabled = false;
            gameStartCountDownSeconds = startCountDownTime;
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

    /// <summary>
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// アタッチされている場合は破棄する。
    /// </summary>
    private void CheckInstance()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
