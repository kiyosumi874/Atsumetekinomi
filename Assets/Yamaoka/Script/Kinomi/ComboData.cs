using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コンボのデータ
/// </summary>
[System.Serializable]
public class ComboData
{
    public string comboName;    // コンボ名
    public int comboCount;      // コンボ使用回数
    public int firstComboScore = 500;   // 最初に使用したときのスコア
    public int normalComboScore = 100;  // 通常時のスコア
    public int comboLevel = 1;      // コンボレベル
    public int comboLvUpCount = 5;
    public bool isFrist = true;     // 初めて使用するかどうか
    public bool useCombo = false;   // コンボを使えるかどうか
    public int useAppleNum = 0;
    public int useOrengeNum = 0;
    public int useBananaNum = 0;
    public int useRemonNum = 0;
    //public bool useApple;
    //public bool useOrenge;
    //public bool useBanana;
    //public bool useRemon;

    /// <summary>
    /// コンボを使用する
    /// </summary>
    /// <param name="_isFirst">使用するのが初めてかどうか</param>
    /// <param name="score">スコア</param>
    public int UseCombo(bool _isFirst, int score)
    {
        if(_isFirst)
        {
            score += firstComboScore;
        }
        else
        {
            score += normalComboScore * comboLevel;
        }
        comboCount++;

        Debug.Log(comboName + "が発動");
        Debug.Log("スコア：" + score);

        return score;
    }
}
