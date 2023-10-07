using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �\������R���{�G�t�F�N�g�̃f�[�^
/// </summary>
[System.Serializable]
public class ComboEffectData
{
    public string comboWard;
    public Color color;
    public string firstBonus;
    public string normalScore;
    public string score;
    public bool isFirst = true;
}

/// <summary>
/// �R���{�G�t�F�N�g����N���X
/// </summary>
public class ComboEffectManager : MonoBehaviour
{
    public List<ComboEffectData> effectDatas = new List<ComboEffectData>();
    public List<string> comboWards = new List<string>();
    public GameObject comboObj;
    public Text comboText;
    public Text firstBonusText;
    RectTransform comboRectTransform;

    [SerializeField]
    AnimationCurve scaleCurve;
    [SerializeField, Range(0.2f, 0.5f)]
    float initEffectDuration = 0.2f;    // �������o����
    [SerializeField, Range(0.1f, 1.0f)]
    float durationIncrement = 0.3f;    // ��������
    [SerializeField, Range(1.0f, 5.0f)]
    float maxEffectDuration = 1.4f;     // �ő剉�o����
    [SerializeField, Range(1.2f, 1.5f)]
    float initMaxScale = 1.4f;          // �����ő�X�P�[��
    [SerializeField, Range(0.2f, 1.0f)]
    float scaleIncrement = 0.6f;        // �X�P�[��������
    [SerializeField, Range(5.0f, 10.0f)]
    float maxScale = 5.0f;              // �ő�X�P�[��
    [SerializeField, Range(0.0f, 0.2f)]
    float basicScaleIncrement = 0.09f;  // ���o��̕����̑傫���̑�����

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
        firstBonusText.enabled = false;
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
        // �R���{�����傫�����Z���ԂŎ��̕\�����s��
        var tempRate = Mathf.Clamp((1.0f - counter/* / 10.0f*/), 2.5f, 5.0f);
        if (timer > effectDuration * tempRate)
        {
            timer = 0;
            UpdateEffectData(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// �R���{���̑����v�Z
    /// </summary>
    public void IncreaseCombo()
    {
        //counter++;
        comboOrder.Enqueue(counter);
        // ����̂�
        //if (counter == 0)
        //{
        //    UpdateEffectData(comboOrder.Dequeue());
        //}
    }

    /// <summary>
    /// �R���{�G�t�F�N�g�f�[�^�̍X�V
    /// </summary>
    /// <param name="comboCount"></param>
    public void UpdateEffectData(int comboCount)
    {
        Show();
        //comboText.text = comboWards[comboCount];
        comboText.text = effectDatas[comboCount].comboWard;
        comboText.color = effectDatas[comboCount].color;

        firstBonusText.text = effectDatas[comboCount].score;
        firstBonusText.color = effectDatas[comboCount].color;
        effectDatas[comboCount].isFirst = false;
        //Debug.Log(comboText.text);
        comboRectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-15.0f, 15.0f));

        // �O�̃R���{���o���I�����Ă��Ȃ��ꍇ
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
    /// �R���{�G�t�F�N�g�Ɏg�p����R���{�̖��O���擾���A�ݒ肷��
    /// </summary>
    /// <param name="comboDatas">�R���{�f�[�^</param>
    public void SetComboName(ComboData comboDatas)
    {
        for (int i = 0; i < effectDatas.Count; i++)
        {
            //counter = i;
            //if (comboWards[i] == comboDatas.comboName)
            //{
            //    counter = i;
            //    //comboText.text = comboWards[i];
            //    //Debug.Log(comboWards[i]);
            //    SetFirstComboText(comboDatas);
            //    IncreaseCombo();
            //}
            if (effectDatas[i].comboWard == comboDatas.comboName)
            {
                counter = i;
                //comboText.text = comboWards[i];
                //Debug.Log(comboWards[i]);
                SetFirstComboText(comboDatas);
                IncreaseCombo();
            }
        }
    }

    public void SetFirstComboText(ComboData comboDatas)
    {
        for (int i = 0; i < effectDatas.Count; i++)
        {
            effectDatas[i].normalScore =
                    "+" + (comboDatas.normalComboScore * comboDatas.comboLevel).ToString();
            if (effectDatas[i].isFirst)
            {
                effectDatas[i].score = effectDatas[i].firstBonus + "   +" + comboDatas.firstComboScore;
                //firstBonusText.color = effectDatas[i].color;
                Debug.Log("TRUEEEEEEEEEEEEEEEEEEEEEEEE");
            }
            else
            {
                effectDatas[i].score = effectDatas[i].normalScore;
                //effectDatas[i].normalScore = 
                //    "+" + (comboDatas.normalComboScore * comboDatas.comboLevel).ToString();
                Debug.Log("FALSEEEEEEEEEEEEEEEEEEEEEEEE");
            }
        }
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
        firstBonusText.enabled = true;
    }

    void Hide()
    {
        comboText.enabled = false;
        firstBonusText.enabled = false;
    }

    void Clear()
    {
        counter = 0;
        comboOrder.Clear();
        Hide();
    }
}
