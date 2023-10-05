using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboEffectSystem : MonoBehaviour
{
    [SerializeField]
    string comboWard;
    [SerializeField]
    GameObject comboObj;
    [SerializeField]
    AnimationCurve scaleCurve;
    [SerializeField, Range(0.2f, 0.5f)]
    float initEffectDuration = 0.2f;    // 初期演出時間
    [SerializeField, Range(0.01f, 0.1f)]
    float durationIncrement = 0.03f;    // 増加時間
    [SerializeField, Range(0.4f, 0.7f)]
    float maxEffectDuration = 0.4f;     // 最大演出時間
    [SerializeField, Range(1.2f, 1.5f)]
    float initMaxScale = 1.4f;          // 初期最大スケール
    [SerializeField, Range(0.2f, 1.0f)]
    float scaleIncrement = 0.6f;        // スケール増加量
    [SerializeField, Range(5.0f, 10.0f)]
    float maxScale = 5.0f;              // 最大スケール
    [SerializeField, Range(0.0f, 0.2f)]
    float basicScaleIncrement = 0.09f;  // 演出後の文字の大きさの増加量

    Text comboText;
    RectTransform comboRectTransform;

    int counter = 0;
    bool playingEffect = false;
    float scale;
    float basicScale;
    float effectDuration;
    float timer = 0.0f;
    Coroutine effectCol;
    Queue<int> comboOrder = new Queue<int>();

    private void Awake()
    {
        comboText = comboObj.GetComponent<Text>();
        comboRectTransform = comboObj.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(comboOrder.Count == 0)
        {
            return;
        }

        timer += Time.deltaTime;
        // コンボ数が大きい程短時間で次の表示を行う
        var tempRate = Mathf.Clamp((1.0f - counter / 10.0f), 0.3f, 0.5f);
        if(timer > effectDuration * tempRate)
        {
            timer = 0;
            UpdateCombo(comboOrder.Dequeue());
        }
    }


    public void IncreaseCombo()
    {
        counter++;
        comboOrder.Enqueue(counter);
        // 初回のみ
        if(counter == 1)
        {
            UpdateCombo(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// コンボの更新
    /// </summary>
    /// <param name="comboCount">コンボ数</param>
    void UpdateCombo(int comboCount)
    {
        Show();
        comboText.text = comboCount + comboWard;

        comboRectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-15.0f, 15.0f));

        // 前のコンボ演出が終了していない場合
        if(playingEffect)
        {
            StopCoroutine(effectCol);

            if(effectDuration < maxEffectDuration)
            {
                effectDuration += durationIncrement;
            }
            if(scale < maxScale)
            {
                scale += scaleIncrement;
            }
            // ７コンボまでは初期スケールを大きくする
            if(counter < 7)
            {
                basicScale += basicScaleIncrement;
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

    void Show()
    {
        comboText.enabled = true;
    }

    void Hide()
    {
        comboText.enabled = false;
    }

    void Clear()
    {
        counter = 0;
        comboOrder.Clear();
        Hide();
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
        while(rate < 1)
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
}
