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
    float initEffectDuration = 0.2f;    // �������o����
    [SerializeField, Range(0.01f, 0.1f)]
    float durationIncrement = 0.03f;    // ��������
    [SerializeField, Range(0.4f, 0.7f)]
    float maxEffectDuration = 0.4f;     // �ő剉�o����
    [SerializeField, Range(1.2f, 1.5f)]
    float initMaxScale = 1.4f;          // �����ő�X�P�[��
    [SerializeField, Range(0.2f, 1.0f)]
    float scaleIncrement = 0.6f;        // �X�P�[��������
    [SerializeField, Range(5.0f, 10.0f)]
    float maxScale = 5.0f;              // �ő�X�P�[��
    [SerializeField, Range(0.0f, 0.2f)]
    float basicScaleIncrement = 0.09f;  // ���o��̕����̑傫���̑�����

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
        // �R���{�����傫�����Z���ԂŎ��̕\�����s��
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
        // ����̂�
        if(counter == 1)
        {
            UpdateCombo(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// �R���{�̍X�V
    /// </summary>
    /// <param name="comboCount">�R���{��</param>
    void UpdateCombo(int comboCount)
    {
        Show();
        comboText.text = comboCount + comboWard;

        comboRectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-15.0f, 15.0f));

        // �O�̃R���{���o���I�����Ă��Ȃ��ꍇ
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
            // �V�R���{�܂ł͏����X�P�[����傫������
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
    /// ���o�A�e�L�X�g�̑傫����偨���ɂ���
    /// </summary>
    /// <param name="duration">�ω�����(second)</param>
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
