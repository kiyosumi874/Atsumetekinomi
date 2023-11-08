using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// �Q�[���̏�Ԃ�ݒ�
/// </summary>
public enum GameState
{
    BeforeGame,     // �Q�[���J�n�O
    InGame,         // �Q�[����
    AfterGame,      // �Q�[���I��
    None,
}

public class GameManager : MonoBehaviour
{
    // �Q�[���J�n�܂ł̃J�E���g�_�E���p
    public float gameStartCountDownSeconds;
    // �Q�[�����̐������Ԑݒ�p
    public float countDownSeconds;

    // �e���Ԃ̏����l��ۑ�����ϐ�
    public float startCountDownTime;
    public float gameTime;

    public GameState gameState = GameState.BeforeGame;

    public int score = 0;   // �ŏI�X�R�A

    public static GameManager instance;      // �C���X�^���X


    private void Awake()
    {
        CheckInstance();
        //gameState = GameState.BeforeGame;
        // �e���Ԃ̏����l��ݒ�
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
                // �X�R�A������������
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
                // �e���Ԃ�����������
                gameStartCountDownSeconds = startCountDownTime;
                countDownSeconds = gameTime;
                // ���݂̃V�[�����Q�[���V�[���̎�
                if (SceneManager.GetActiveScene().name == "TestGameScene")
                {
                    ChangeGameState(GameState.BeforeGame);
                }
                break;
        }
    }

    /// <summary>
    /// �Q�[���V�[�����烊�U���g�V�[���ɑJ�ڂ���ۂɌĂ�
    /// </summary>
    public void ChangeGameToResult()
    {
        ChangeGameState(GameState.AfterGame);
        //gameStartCountDownSeconds = startCountDownTime;
        //countDownSeconds = gameTime;
    }

    /// <summary>
    /// �Q�[���V�[������^�C�g���V�[���ɑJ�ڂ���ۂɌĂ�
    /// </summary>
    public void ChangeGameToTitle()
    {
        ChangeGameState(GameState.None);
        //gameStartCountDownSeconds = startCountDownTime;
        //countDownSeconds = gameTime;
    }

    /// <summary>
    /// �Q�[���X�e�[�^�X�̕ύX���s��
    /// </summary>
    /// <param name="nextGameState">���̃Q�[���X�e�[�^�X</param>
    public void ChangeGameState(GameState nextGameState)
    {
        gameState = nextGameState;
    }

    /// <summary>
    /// �Q�[�����̎��Ԍv�����s��
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
    /// �Q�[���J�n�O�̃J�E���g�_�E������
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
    /// �X�R�A�̎󂯓n�����s��
    /// </summary>
    /// <returns>�Q�[�����Ɋl�������X�R�A</returns>
    public int ResultScore()
    {
        score = ComboManager.instance.score;
        return score;
    }

    /// <summary>
    /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
    /// �A�^�b�`����Ă���ꍇ�͔j������B
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
