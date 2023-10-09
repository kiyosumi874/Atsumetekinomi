using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;     // �l�������X�R�A��\��


    string[] ranking = { "1��", "2��", "3��", "4��", "5��" };
    int[] rankingValue = new int[5];

    [SerializeField]
    Text[] rankingText = new Text[5];   // �����L���O��\������e�L�X�g

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ChangeGameState(GameState.None);

        GetRanking();
        SetRanking(GameManager.instance.score);

        scoreText.text = GameManager.instance.score.ToString();

        for(int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = ranking[i] + "  :  " +rankingValue[i].ToString();
        }
    }

    /// <summary>
    /// �����L���O�Ăяo��
    /// </summary>
    void GetRanking()
    {
        for(int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    /// <summary>
    /// �����L���O�ݒ�
    /// </summary>
    /// <param name="score">�l�������X�R�A</param>
    void SetRanking(int score)
    {
        CheckHightScore(score);
        for ( int i = 0;i < ranking.Length; i++)
        {
            // �l���X�R�A�ƃ����L���O���̒l���r���ē���ւ�
            if(score > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = score;
                score = change;
            }
        }
        // ����ւ����l��ۑ�
        for(int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }

        Debug.Log(PlayerPrefs.GetInt(ranking[0]));
    }

    /// <summary>
    /// �g�b�v�X�R�A���X�V���������`�F�b�N
    /// </summary>
    /// <param name="score">�l�������X�R�A</param>
    public void CheckHightScore(int score)
    {
        if (PlayerPrefs.GetInt(ranking[0]) <= score)
        {
            Debug.Log("HIGHT SCORE !!!!!!!!!!!!!!!!!!!");
        }
    }

    /// <summary>
    /// �����L���O�f�[�^���폜
    /// </summary>
    public void DeleteRankingData()
    {
        PlayerPrefs.DeleteAll();
    }
}
