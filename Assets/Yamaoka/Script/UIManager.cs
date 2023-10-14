using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

/// <summary>
/// コンボUIのデータ
/// </summary>
[System.Serializable]
public class ComboUIData
{
    public string comboName;        // コンボの名前
    public GameObject comboUI;      // UIのオブジェクト
    public Text comboLvText;        // レベルを表示するテキスト
    public Text comboNoText;        // コンボ番号を表示するテキスト
    public Text addScore;           // 加算されるスコアを表示するテキスト
    public int comboUINomber = 0;   // コンボUIの番号(上から何番目か)
}

/// <summary>
/// UIマネージャー
/// </summary>
public class UIManager : MonoBehaviour
{
    // コンボUIデータを格納
    public List<ComboUIData> comboUIDataList = new List<ComboUIData>();

    public static UIManager instance;   // インスタンス

    public Text countDownText;
    public Text gameTimeText;

    //すべての木の実の画像を格納
    public List<UnityEngine.UI.Image> appleImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> lemonImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> orangeImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> bananaImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> watermelonImgs = new List<UnityEngine.UI.Image>();

    // 操作パネル
    public GameObject operationPanel;

    private void Awake()
    {
        instance = this;
        //InitComboUI();
        countDownText.enabled = false;
    }

    /// <summary>
    /// コンボUIの初期化
    /// </summary>
    public void InitComboUI()
    {
        for(int i = 0; i < comboUIDataList.Count; i++)
        {
            comboUIDataList[i].comboUI.SetActive(false);
        }
    }

    /// <summary>
    /// コンボUIを表示
    /// </summary>
    /// <param name="comboDatas"></param>
    public void ShowComboUI(ComboData[] comboDatas)
    {
        foreach (ComboUIData data in comboUIDataList)
        {
            for (int i = 0; i < comboDatas.Length; i++)
            {
                //if (comboDatas[i].comboName == data.comboName
                //    && !comboDatas[i].isFrist)
                //{
                //    // 使用されたコンボのUIを表示する
                //    data.comboUI.SetActive(true);
                //}

                if(comboDatas[i].comboName == data.comboName)
                {
                    // 各UIの数字を更新
                    data.comboLvText.text = "Lv ." + comboDatas[i].comboLevel.ToString();
                    data.addScore.text = "+" + (comboDatas[i].normalComboScore * comboDatas[i].comboLevel).ToString();
                    data.comboNoText.text = "(" + data.comboUINomber + "/" + comboDatas.Length + ")";
                }
            }
        }
    }

    /// <summary>
    /// コンボUIの拡大・縮小アニメーション
    /// </summary>
    /// <param name="comboDatas">コンボデータ</param>
    public void ChangeComboUIScale(ComboData[] comboDatas)
    {
        foreach (ComboUIData data in comboUIDataList)
        {
            RectTransform rectTransform;
            rectTransform = data.comboUI.GetComponent<RectTransform>();
            var startScale = new Vector3(0.13f, 0.13f, 1);
            var endScale = new Vector3(0.14f, 0.14f, 1);
            for (int i = 0; i < comboDatas.Length; i++)
            {
                if (comboDatas[i].useCombo
                    && comboDatas[i].comboName == data.comboName
                    && !comboDatas[i].isFrist)
                {
                    if (rectTransform.localScale.x <= startScale.x)
                    {
                        rectTransform.DOScale(
                            endScale,
                            1.0f);
                    }
                    if (rectTransform.localScale.x >= endScale.x)
                    {
                        rectTransform.DOScale(
                            startScale,
                            1.0f);
                    }
                }
                else if(!comboDatas[i].useCombo
                    && comboDatas[i].comboName == data.comboName)
                {
                    data.comboUI.GetComponent<RectTransform>().localScale = startScale;
                }
            }

        }
    }

    /// <summary>
    /// 木の実画像の透過度を設定
    /// </summary>
    /// <param name="apple">hasApple</param>
    /// <param name="lemon">hasLemon</param>
    /// <param name="orange">hasOrange</param>
    /// <param name="banana">hasBanana</param>
    /// <param name="watermelon">hasWatermelon</param>
    public void ChangeKinomiImgAlpha(bool apple, bool lemon, bool orange, bool banana, bool watermelon)
    {
        Color hasNotKinomi = new Color(1, 1, 1, 0.8f);  // 木の実を持っていないときの透過度
        Color hasKinomi = new Color(1, 1, 1, 1.0f);     // 木の実を持っているときの透過度
        // リンゴ画像の色を設定
        for (int i = 0; i < appleImgs.Count; i++)
        {
            if(apple)
            {
                appleImgs[i].color = hasKinomi;
            }
            else
            {
                appleImgs[i].color = hasNotKinomi;
            }
        }
        // レモン画像の色を設定
        for (int i = 0; i < lemonImgs.Count; i++)
        {
            if (lemon)
            {
                lemonImgs[i].color = hasKinomi;
            }
            else
            {
                lemonImgs[i].color = hasNotKinomi;
            }
        }
        // オレンジ画像の色を設定
        for (int i = 0; i < orangeImgs.Count; i++)
        {
            if (orange)
            {
                orangeImgs[i].color = hasKinomi;
            }
            else
            {
                orangeImgs[i].color = hasNotKinomi;
            }
        }
        // バナナ画像の色を設定
        for (int i = 0; i < bananaImgs.Count; i++)
        {
            if (banana)
            {
                bananaImgs[i].color = hasKinomi;
            }
            else
            {
                bananaImgs[i].color = hasNotKinomi;
            }
        }
        // スイカ画像の色を設定
        for (int i = 0; i < watermelonImgs.Count; i++)
        {
            if (watermelon)
            {
                watermelonImgs[i].color = hasKinomi;
            }
            else
            {
                watermelonImgs[i].color = hasNotKinomi;
            }
        }
    }
}
