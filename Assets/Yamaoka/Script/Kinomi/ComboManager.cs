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
    /// �R���{���g�p�ł��邩�ǂ����𒲂ׂ�
    /// </summary>
    public void CheckUseCombo()
    {
        for(int i = 0; i < comboDatas.Length; i++)
        {
            // �g�p�\�Ȃ�AcomboDataList�ɒǉ�
            // �d���͂����Ȃ�
            if(!comboDataList.Contains(comboDatas[i])
                && comboDatas[i].useCombo)
            {
                comboDataList.Add(comboDatas[i]);
            }
        }
    }

    /// <summary>
    /// �g�p�ł��Ȃ��Ȃ����R���{��List����폜
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
    /// �R���{���g�p����
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
    /// ��������R���{���Ȃ��؂̎��̃X�R�A
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
    /// �R���{����g�p�t���O�̐ݒ�
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
    /// �e�R���{�̎g�p�t���O�̐ݒ�
    /// </summary>
    public void SetUseComboFlag()
    {
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if(KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge)
            {
                if(comboDatas[i].comboName == "�����S�I�����W")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�����S�I�����W")
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
                if (comboDatas[i].comboName == "�I�����W�o�i�i")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�I�����W�o�i�i")
                {
                    comboDatas[i].useCombo = false;
                }
            }
        }
    }

}
