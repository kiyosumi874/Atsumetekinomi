using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private List<ComboData> comboDataList = new List<ComboData>();

    [SerializeField]
    ComboData[] comboDatas;

    public int score = 0;
    public Text scoreText;

    public static ComboManager instance;
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
        if(KinomiManager.instance.appleNum > 1
            || KinomiManager.instance.orengeNum > 1
            || KinomiManager.instance.bananaNum > 1)
        {
            score +=
                Kinomi.instance.score * (KinomiManager.instance.appleNum - 1 + KinomiManager.instance.orengeNum - 1 + KinomiManager.instance.bananaNum - 1);
        }
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
        }
        for (int i = 0; i < comboDatas.Length; i++)
        {
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
