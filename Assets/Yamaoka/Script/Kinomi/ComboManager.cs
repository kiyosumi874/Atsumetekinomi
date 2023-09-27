using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �R���{�}�l�[�W���[
/// </summary>
public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private List<ComboData> comboDataList = new List<ComboData>();  // �g�p�\�ȃR���{�i�[�pList

    [SerializeField]
    ComboData[] comboDatas;   // �R���{�f�[�^�i�[�pList

    public int score = 0;     // �X�R�A
    public Text scoreText;

    public static ComboManager instance;    // �C���X�^���X

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
        // �g����R���{���Ȃ��ꍇ
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
        // �g����R���{������A���؂̎����]���Ă���ꍇ
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
        Debug.Log("�擾�X�R�A(�؂̎��̂�)�F" + score);
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
