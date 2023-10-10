using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 表示するコンボエフェクトのデータ
/// </summary>
[System.Serializable]
public class ComboEffectData
{
    public string comboWard;    // 発動するコンボの名前
    public Color color;         // 表示する際のエフェクトの色
    public string firstBonus;   // 初回ボーナス時の表示エフェクト設定用
    public string normalScore;  // 通常時の表示エフェクト設定用
    public string effect;        // 表示するエフェクト
    public bool isFirst = true;  // 初回かどうかを判定
}

/// <summary>
/// コンボエフェクト制御クラス
/// </summary>
public class ComboEffectManager : MonoBehaviour
{
    public List<ComboEffectData> effectDatas = new List<ComboEffectData>();
    public GameObject comboObj;
    public Text comboText;
    public Text getScoreText;
    RectTransform comboRectTransform;

    [SerializeField]
    AnimationCurve scaleCurve;
    [SerializeField, Range(0.2f, 0.5f)]
    float initEffectDuration = 0.2f;    // 初期演出時間
    [SerializeField, Range(0.1f, 1.0f)]
    float durationIncrement = 0.3f;    // 増加時間
    [SerializeField, Range(1.0f, 5.0f)]
    float maxEffectDuration = 1.4f;     // 最大演出時間
    [SerializeField, Range(1.2f, 1.5f)]
    float initMaxScale = 1.4f;          // 初期最大スケール
    [SerializeField, Range(0.2f, 1.0f)]
    float scaleIncrement = 0.6f;        // スケール増加量
    [SerializeField, Range(5.0f, 10.0f)]
    float maxScale = 5.0f;              // 最大スケール
    [SerializeField, Range(0.0f, 0.2f)]
    float basicScaleIncrement = 0.09f;  // 演出後の文字の大きさの増加量

    [SerializeField]
    int counter = 0;
    bool playingEffect = false;
    float scale;
    float basicScale;
    float effectDuration;
    float timer = 0.0f;
    Coroutine effectCol;
    Queue<int> comboOrder = new Queue<int>();

    public static ComboEffectManager instance;

    private void Awake()
    {
        getScoreText.enabled = false;
        comboText = comboObj.GetComponent<Text>();
        comboRectTransform = comboObj.GetComponent<RectTransform>();
        instance = this;
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (comboOrder.Count == 0)
        {
            return;
        }

        timer += Time.deltaTime;
        // コンボ数が大きい程短時間で次の表示を行う
        var tempRate = Mathf.Clamp((1.0f - counter / 10.0f), 2.0f, 5.0f);
        if (timer > effectDuration * tempRate)
        {
            timer = 0;
            UpdateEffectData(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// コンボ数の増加計算
    /// </summary>
    public void IncreaseCombo()
    {
        comboOrder.Enqueue(counter);
        // 初回のみ
        if (counter == 0)
        {
            UpdateEffectData(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// コンボエフェクトデータの更新
    /// </summary>
    /// <param name="comboCount"></param>
    public void UpdateEffectData(int comboCount)
    {
        SoundManager.instance.PlaySound("コンボ");
        Show();
        comboText.text = effectDatas[comboCount].comboWard;
        comboText.color = effectDatas[comboCount].color;

        getScoreText.text = effectDatas[comboCount].effect;
        getScoreText.color = effectDatas[comboCount].color;
        effectDatas[comboCount].isFirst = false;

        comboRectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-15.0f, 15.0f));

        // 前のコンボ演出が終了していない場合
        if (playingEffect)
        {
            StopCoroutine(effectCol);

            if (effectDuration < maxEffectDuration)
            {
                effectDuration += durationIncrement;
            }
            if (scale < maxScale)
            {
                scale += scaleIncrement;
            }
        }
        else
        {
            scale = initMaxScale;
            basicScale = 1;
            effectDuration = initEffectDuration;
        }

        effectCol = StartCoroutine(PlayEffect(effectDuration));
    }

    /// <summary>
    /// コンボが発動しないときのエフェクト
    /// </summary>
    /// <param name="score">取得するスコア</param>
    public void SetNoComboEffect(int score)
    {
        // effectDatas[0](ノーコンボデータ)を設定
        effectDatas[0].effect = "スコア  +" + score;
        effectDatas[0].comboWard = "きのみだけ";
        // 取得できるスコアが0以上の時
        if(score != 0)
        {
            // effectDatas[0](ノーコンボデータ)エフェクトを再生
            counter = 0;
            IncreaseCombo();

            //Debug.Log("NO COMBOOOOOOOOOOOOOOOOOOOO");
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// コンボエフェクトに使用するコンボの名前を取得し、設定する
    /// </summary>
    /// <param name="comboDatas">コンボデータ</param>
    public void SetComboName(ComboData comboDatas)
    {
        for (int i = 1; i < effectDatas.Count; i++)
        {
            if (effectDatas[i].comboWard == comboDatas.comboName)
            {
                counter = i;
                SetFirstComboText(comboDatas);
                IncreaseCombo();
            }
        }
    }

    /// <summary>
    /// 初回時のエフェクトを設定
    /// </summary>
    /// <param name="comboDatas">コンボデータ</param>
    public void SetFirstComboText(ComboData comboDatas)
    {
        for (int i = 1; i < effectDatas.Count; i++)
        {
            // 通常時に表示するスコアの値を計算
            effectDatas[i].normalScore =
                    "+" + (comboDatas.normalComboScore * comboDatas.comboLevel).ToString();
            // コンボエフェクトが発動するのが初めての時
            if (effectDatas[i].isFirst)
            {
                // 初回用のエフェクトを表示
                effectDatas[i].effect = effectDatas[i].firstBonus + "   +" + comboDatas.firstComboScore;
            }
            // 初めてではないとき
            else
            {
                // 通常時のエフェクトを表示
                effectDatas[i].effect = effectDatas[i].normalScore;
            }
        }
    }

    /// <summary>
    /// 演出、テキストの大きさを大→小にする
    /// </summary>
    /// <param name="duration">変化時間(second)</param>
    /// <returns></returns>
    IEnumerator PlayEffect(float duration)
    {
        var timer = 0.0f;
        var rate = 0.0f;
        var startScale = new Vector3(scale, scale, 1);
        var endScale = new Vector3(basicScale, basicScale, 1);

        playingEffect = true;
        while (rate < 1)
        {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer / duration);
            var curvePos = scaleCurve.Evaluate(rate);
            comboRectTransform.localScale = Vector3.Lerp(startScale, endScale, curvePos);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Clear();
        playingEffect = false;
    }

    void Show()
    {
        comboText.enabled = true;
        getScoreText.enabled = true;
    }

    void Hide()
    {
        comboText.enabled = false;
        getScoreText.enabled = false;
    }

    void Clear()
    {
        counter = 0;
        comboOrder.Clear();
        Hide();
    }
}
