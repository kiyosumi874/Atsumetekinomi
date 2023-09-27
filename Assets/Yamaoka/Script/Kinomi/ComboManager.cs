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
                score = comboDatas[i].UseCombo(comboDatas[i].isFrist, score);
            }
        }
        GetNotComboScore();
    }

    /// <summary>
    /// 発動するコンボがない木の実のスコア
    /// </summary>
    public void GetNotComboScore()
    {
        // 使えるコンボがない場合
        if(comboDataList.Count == 0)
        {
            if (KinomiManager.instance.appleNum >= 1 )
            {
                score += Kinomi.instance.score * KinomiManager.instance.appleNum;
            }
            if (KinomiManager.instance.orengeNum >= 1)
            {
                score += Kinomi.instance.score * KinomiManager.instance.orengeNum;
            }
            if (KinomiManager.instance.bananaNum >= 1 )
            {
                score += Kinomi.instance.score * KinomiManager.instance.bananaNum;
            }
        }
        // 使えるコンボがあり、かつ木の実が余っている場合
        else
        {
            if (KinomiManager.instance.appleNum - 1 >= 1)
            {
                score += Kinomi.instance.score * (KinomiManager.instance.appleNum - 1);
            }
            if (KinomiManager.instance.orengeNum - 1 >= 1)
            {
                score += Kinomi.instance.score * (KinomiManager.instance.orengeNum - 1);
            }
            if (KinomiManager.instance.bananaNum - 1 >= 1)
            {
                score += Kinomi.instance.score * (KinomiManager.instance.bananaNum - 1);
            }
        }
        Debug.Log("取得スコア(木の実のみ)：" + score);
    }

    /// <summary>
    /// コンボ初回使用フラグの設定
    /// </summary>
    public void SetFirstFlag()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(comboDatas[i].comboCount <= 0)
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

}
