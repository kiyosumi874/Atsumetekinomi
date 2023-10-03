using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コンボマネージャー
/// </summary>
public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private List<ComboData> comboDataList = new List<ComboData>();  // 使用可能なコンボ格納用List

    [SerializeField]
    ComboData[] comboDatas;   // コンボデータ格納用List

    public int score = 0;     // スコア
    public Text scoreText;

    // コンボUI
    public Text[] comboText;
    public Text[] comboLvText;
    public Text[] comboNoText;
    public int nowComboNum = 0;

    public static ComboManager instance;    // インスタンス

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < comboText.Length; i++)
        {
            comboText[i].enabled = false;
            comboLvText[i].enabled = false;
            comboNoText[i].enabled = false;
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        CheckUseCombo();
        SetFirstFlag();
        SetUseComboFlag();
        DeleteCombo();
        UpdateComboLevel();
        ShowComboUI();
    }

    /// <summary>
    /// コンボが使用できるかどうかを調べる
    /// </summary>
    public void CheckUseCombo()
    {
        for(int i = 0; i < comboDatas.Length; i++)
        {
            // 使用可能なら、comboDataListに追加
            // 重複はさせない
            if(!comboDataList.Contains(comboDatas[i])
                && comboDatas[i].useCombo)
            {
                comboDataList.Add(comboDatas[i]);
            }
        }
    }

    /// <summary>
    /// 使用できなくなったコンボをListから削除
    /// </summary>
    public void DeleteCombo()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(!comboDatas[i].useCombo)
            {
                comboDataList.Remove(comboDatas[i]);
            }
        }
    }

    /// <summary>
    /// コンボを使用する
    /// </summary>
    public void UseCombo()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(comboDatas[i].useCombo)
            {
                score = 
                    comboDatas[i].UseCombo(comboDatas[i].isFrist, score);
                SubtractionUseComboKinomi(comboDatas[i]);
            }
        }
        GetNotComboScore();
    }

    /// <summary>
    /// コンボで使用された木の実を減算する
    /// </summary>
    public void SubtractionUseComboKinomi(ComboData comboData)
    {
        switch(comboData.comboName)
        {
            case "リンゴオレンジ":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                break;
            case "オレンジバナナ":
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                break;
        }
    }

    /// <summary>
    /// 発動するコンボがない木の実のスコア
    /// </summary>
    public void GetNotComboScore()
    {
        int kinomiScore = 0;
        // 使えるコンボがない場合
        if(comboDataList.Count == 0)
        {
            if (KinomiManager.instance.appleNum >= 1 )
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.appleNum;
            }
            if (KinomiManager.instance.orengeNum >= 1)
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.orengeNum;
            }
            if (KinomiManager.instance.bananaNum >= 1 )
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.bananaNum;
            }
            if(KinomiManager.instance.lemonNum >= 1)
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.lemonNum;
            }
            if (KinomiManager.instance.watermelonNum >= 1)
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.watermelonNum;
            }
        }
        // 使えるコンボがあり、かつ木の実が余っている場合
        else
        {
            foreach (ComboData data in comboDataList)
            {
                kinomiScore += OverKinomiScore(data);
            }
        }
        Debug.Log("取得スコア(木の実のみ)：" + kinomiScore);
        // 現在のスコアに加算
        score += kinomiScore;
        Debug.Log("合計スコア：" + score);
    }

    /// <summary>
    /// コンボで使用されずに余った木の実のスコアを計算
    /// </summary>
    /// <param name="score">現在の木の実スコア</param>
    /// <param name="comboData">コンボデータ</param>
    /// <returns>計算された木の実スコア</returns>
    public int OverKinomiScore(ComboData comboData)
    {
        int score = 0;
        if (KinomiManager.instance.appleNum >= comboData.useAppleNum
            && KinomiManager.instance.hasApple)
        {
            score += Kinomi.instance.score * (KinomiManager.instance.appleNum);
            KinomiManager.instance.appleNum -= 1;
            Debug.Log("AppleNum : " + KinomiManager.instance.appleNum);
            Debug.Log("Apple : " + score);
        }
        if (KinomiManager.instance.orengeNum >= comboData.useOrengeNum
            && KinomiManager.instance.hasOrenge)
        {
            score += Kinomi.instance.score * (KinomiManager.instance.orengeNum);
            KinomiManager.instance.orengeNum -= 1;
            Debug.Log("OrengeNum : " + KinomiManager.instance.orengeNum);
            Debug.Log("Orange : " + score);
        }
        if (KinomiManager.instance.bananaNum >= comboData.useBananaNum
            && KinomiManager.instance.hasBanana)
        {
            score += Kinomi.instance.score * (KinomiManager.instance.bananaNum);
            KinomiManager.instance.bananaNum -= 1;
            Debug.Log("BananaNum : " + KinomiManager.instance.bananaNum);
            Debug.Log("Banana : " + score);
        }
        if (KinomiManager.instance.lemonNum >= comboData.useLemonNum
            && KinomiManager.instance.hasLemon)
        {
            score += Kinomi.instance.score * (KinomiManager.instance.lemonNum);
            KinomiManager.instance.lemonNum -= 1;
            Debug.Log("LemonNum : " + KinomiManager.instance.lemonNum);
            Debug.Log("Lemon : " + score);
        }
        if (KinomiManager.instance.watermelonNum >= comboData.useWatermelonNum
            && KinomiManager.instance.hasWatermelon)
        {
            score += Kinomi.instance.score * (KinomiManager.instance.watermelonNum);
            KinomiManager.instance.watermelonNum -= 1;
            Debug.Log("WatermelonNum : " + KinomiManager.instance.watermelonNum);
            Debug.Log("Watermelon : " + score);
        }

        Debug.Log("OverKinomiScore : " + score);
        return score;
    }

    /// <summary>
    /// コンボ初回使用フラグの設定
    /// </summary>
    public void SetFirstFlag()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(comboDatas[i].comboCount <= 0
                && comboDatas[i].comboLevel == 1)
            {
                comboDatas[i].isFrist = true;
            }
            else
            {
                comboDatas[i].isFrist = false;
            }
        }
    }

    /// <summary>
    /// 各コンボの使用フラグの設定
    /// </summary>
    public void SetUseComboFlag()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge)
            {
                if(comboDatas[i].comboName == "リンゴオレンジ")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "リンゴオレンジ")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasOrenge
                && KinomiManager.instance.hasBanana)
            {
                if (comboDatas[i].comboName == "オレンジバナナ")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "オレンジバナナ")
                {
                    comboDatas[i].useCombo = false;
                }
            }
        }
    }

    /// <summary>
    /// コンボレベルの更新
    /// </summary>
    public void UpdateComboLevel()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if (comboDatas[i].comboCount > comboDatas[i].comboLvUpCount - 1)
            {
                comboDatas[i].comboLevel++;
                comboDatas[i].comboCount = 0;
            }
        }
    }

    /// <summary>
    /// コンボUIを表示する
    /// </summary>
    public void ShowComboUI()
    {
        int count = 0;
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if (comboDatas[i].comboName == comboText[i].text
                && !comboDatas[i].isFrist)
            {
                comboText[i].enabled = true;
                comboLvText[i].enabled = true;
                comboNoText[i].enabled = true;
            }
            count = i + 1;
            //Debug.Log("count" + count);
            comboLvText[i].text = "Lv ." + comboDatas[i].comboLevel.ToString();
        }
        comboNoText[count - 1].text = "(" + count + "/" + comboDatas.Length + ")";
    }
}
