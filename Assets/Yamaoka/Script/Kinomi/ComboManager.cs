using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

    public static ComboManager instance;    // インスタンス

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        CheckUseCombo();
        SetFirstFlag();
        SetUseComboFlag();
        DeleteCombo();
        UpdateComboLevel();
        UIManager.instance.ShowComboUI(comboDatas);
        //UIManager.instance.ChangeComboUIScale(comboDatas);
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
            if (comboDatas[i].useCombo)
            {
                score =
                    comboDatas[i].UseCombo(comboDatas[i].isFrist, score);
                //SubtractionUseComboKinomi(comboDatas[i]);
                ComboEffectManager.instance.SetComboName(comboDatas[i]);
                ComboEffectManager.instance.SetFirstComboText(comboDatas[i]);
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
            case "リンゴレモン":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                break;
            case "レモンバナナ":
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                break;
            case "まるきのみ":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.watermelonNum -= comboData.useWatermelonNum;
                break;
            case "ぜんぶ":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                KinomiManager.instance.watermelonNum -= comboData.useWatermelonNum;
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
                kinomiScore += KinomiManager.instance.appleScore * KinomiManager.instance.appleNum;
                KinomiManager.instance.appleNum = 0;
            }
            if (KinomiManager.instance.orengeNum >= 1)
            {
                kinomiScore += KinomiManager.instance.orengeScore * KinomiManager.instance.orengeNum;
                KinomiManager.instance.orengeNum = 0;
            }
            if (KinomiManager.instance.bananaNum >= 1 )
            {
                kinomiScore += KinomiManager.instance.bananaScore * KinomiManager.instance.bananaNum;
                KinomiManager.instance.bananaNum = 0;
            }
            if(KinomiManager.instance.lemonNum >= 1)
            {
                kinomiScore += KinomiManager.instance.lemonScore * KinomiManager.instance.lemonNum;
                KinomiManager.instance.lemonNum = 0;
            }
            if (KinomiManager.instance.watermelonNum >= 1)
            {
                kinomiScore += KinomiManager.instance.watermelonScore * KinomiManager.instance.watermelonNum;
                KinomiManager.instance.watermelonNum = 0;
            }
        }
        // 使えるコンボがあり、かつ木の実が余っている場合
        else if (comboDataList.Count > 0
            || KinomiManager.instance.appleNum >= 1
            || KinomiManager.instance.orengeNum >= 1
            || KinomiManager.instance.bananaNum >= 1
            || KinomiManager.instance.lemonNum >= 1
            || KinomiManager.instance.watermelonNum >= 1)
        {
            kinomiScore += OverKinomiScore();
        }
        Debug.Log("取得スコア(木の実のみ)：" + kinomiScore);
        // 現在のスコアに加算
        score += kinomiScore;
        ComboEffectManager.instance.SetNoComboEffect(kinomiScore);
        Debug.Log("合計スコア：" + score);
    }

    /// <summary>
    /// コンボで使用されずに余った木の実のスコアを計算
    /// </summary>
    /// <param name="score">現在の木の実スコア</param>
    /// <param name="comboData">コンボデータ</param>
    /// <returns>計算された木の実スコア</returns>
    public int OverKinomiScore(/*ComboData comboData*/)
    {
        int score = 0;
        //if (KinomiManager.instance.appleNum >= comboData.useAppleNum
        //    && KinomiManager.instance.hasApple)
        if (KinomiManager.instance.appleNum >= 1)
        {
            score += KinomiManager.instance.appleScore * (KinomiManager.instance.appleNum - 1);
            KinomiManager.instance.appleNum = 0;
            //Debug.Log("AppleNum : " + KinomiManager.instance.appleNum);
            //Debug.Log("Apple : " + score);
        }
        //if (KinomiManager.instance.orengeNum >= comboData.useOrengeNum
        //    && KinomiManager.instance.hasOrenge)
        if (KinomiManager.instance.orengeNum >= 1)
        {
            score += KinomiManager.instance.orengeScore * (KinomiManager.instance.orengeNum - 1);
            KinomiManager.instance.orengeNum = 0;
            //Debug.Log("OrengeNum : " + KinomiManager.instance.orengeNum);
            //Debug.Log("Orange : " + score);
        }
        //if (KinomiManager.instance.bananaNum >= comboData.useBananaNum
        //    && KinomiManager.instance.hasBanana)
        if (KinomiManager.instance.bananaNum >= 1)
        {
            score += KinomiManager.instance.bananaScore * (KinomiManager.instance.bananaNum - 1);
            KinomiManager.instance.bananaNum = 0;
            //Debug.Log("BananaNum : " + KinomiManager.instance.bananaNum);
            //Debug.Log("Banana : " + score);
        }
        //if (KinomiManager.instance.lemonNum >= comboData.useLemonNum
        //    && KinomiManager.instance.hasLemon)
        if (KinomiManager.instance.lemonNum >= 1)
        {
            score += KinomiManager.instance.lemonScore * (KinomiManager.instance.lemonNum - 1);
            KinomiManager.instance.lemonNum = 0;
            //Debug.Log("LemonNum : " + KinomiManager.instance.lemonNum);
            //Debug.Log("Lemon : " + score);
        }
        //if (KinomiManager.instance.watermelonNum >= comboData.useWatermelonNum
        //    && KinomiManager.instance.hasWatermelon)
        if (KinomiManager.instance.watermelonNum >= 1)
        {
            score += KinomiManager.instance.watermelonScore * (KinomiManager.instance.watermelonNum - 1);
            KinomiManager.instance.watermelonNum = 0;
            //Debug.Log("WatermelonNum : " + KinomiManager.instance.watermelonNum);
            //Debug.Log("Watermelon : " + score);
        }
        //ComboEffectManager.instance.SetNoComboEffect(score);
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
            if (KinomiManager.instance.hasApple
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


            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasLemon)
            {
                if (comboDatas[i].comboName == "リンゴレモン")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "リンゴレモン")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasBanana
                && KinomiManager.instance.hasLemon)
            {
                if (comboDatas[i].comboName == "レモンバナナ")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "レモンバナナ")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge
                && KinomiManager.instance.hasWatermelon)
            {
                if (comboDatas[i].comboName == "まるきのみ")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "まるきのみ")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge
                && KinomiManager.instance.hasBanana
                && KinomiManager.instance.hasLemon
                && KinomiManager.instance.hasWatermelon)
            {
                if (comboDatas[i].comboName == "ぜんぶ")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "ぜんぶ")
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
}
