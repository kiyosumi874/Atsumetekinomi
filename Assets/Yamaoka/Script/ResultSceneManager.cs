using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;     // 獲得したスコアを表示


    string[] ranking = { "1位", "2位", "3位", "4位", "5位" };
    int[] rankingValue = new int[5];

    [SerializeField]
    Text[] rankingText = new Text[5];   // ランキングを表示するテキスト

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
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        for(int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    /// <summary>
    /// ランキング設定
    /// </summary>
    /// <param name="score">獲得したスコア</param>
    void SetRanking(int score)
    {
        CheckHightScore(score);
        for ( int i = 0;i < ranking.Length; i++)
        {
            // 獲得スコアとランキング内の値を比較して入れ替え
            if(score > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = score;
                score = change;
            }
        }
        // 入れ替えた値を保存
        for(int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }

        Debug.Log(PlayerPrefs.GetInt(ranking[0]));
    }

    /// <summary>
    /// トップスコアを更新したかをチェック
    /// </summary>
    /// <param name="score">獲得したスコア</param>
    public void CheckHightScore(int score)
    {
        if (PlayerPrefs.GetInt(ranking[0]) <= score)
        {
            Debug.Log("HIGHT SCORE !!!!!!!!!!!!!!!!!!!");
        }
    }

    /// <summary>
    /// ランキングデータを削除
    /// </summary>
    public void DeleteRankingData()
    {
        PlayerPrefs.DeleteAll();
    }
}
