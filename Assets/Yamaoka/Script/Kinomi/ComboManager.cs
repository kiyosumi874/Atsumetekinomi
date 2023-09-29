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

    // �R���{UI
    public Text[] comboText;
    public Text[] comboLvText;
    public Text[] comboNoText;
    public int nowComboNum = 0;

    public static ComboManager instance;    // �C���X�^���X

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < comboText.Length; i++)
        {
            comboText[i].enabled = false;
            comboLvText[i].enabled = false;
            comboNoText[i].enabled = false;
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        CheckUseCombo();
        SetFirstFlag();
        SetUseComboFlag();
        DeleteCombo();
        UpdateComboLevel();
        ShowComboUI();
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
        int kinomiScore = 0;
        // �g����R���{���Ȃ��ꍇ
        if(comboDataList.Count == 0)
        {
            if (KinomiManager.instance.appleNum >= 1 )
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.appleNum;
            }
            if (KinomiManager.instance.orengeNum >= 1)
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.orengeNum;
            }
            if (KinomiManager.instance.bananaNum >= 1 )
            {
                kinomiScore += Kinomi.instance.score * KinomiManager.instance.bananaNum;
            }
        }
        // �g����R���{������A���؂̎����]���Ă���ꍇ
        else
        {
            if (KinomiManager.instance.appleNum - 1 >= 1)
            {
                kinomiScore += Kinomi.instance.score * (KinomiManager.instance.appleNum - 1);
            }
            if (KinomiManager.instance.orengeNum - 1 >= 1)
            {
                kinomiScore += Kinomi.instance.score * (KinomiManager.instance.orengeNum - 1);
            }
            if (KinomiManager.instance.bananaNum - 1 >= 1)
            {
                kinomiScore += Kinomi.instance.score * (KinomiManager.instance.bananaNum - 1);
            }
        }
        Debug.Log("�擾�X�R�A(�؂̎��̂�)�F" + kinomiScore);
        // ���݂̃X�R�A�ɉ��Z
        score += kinomiScore;
        Debug.Log("���v�X�R�A�F" + score);
    }

    /// <summary>
    /// �R���{����g�p�t���O�̐ݒ�
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

    /// <summary>
    /// �R���{���x���̍X�V
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

    /// <summary>
    /// �R���{UI��\������
    /// </summary>
    public void ShowComboUI()
    {
        int count = 0;
        for (int i = 0; i < comboDatas.Length; i++)
        {
            if (comboDatas[i].comboName == comboText[i].text
                && !comboDatas[i].isFrist)
            {
                comboText[i].enabled = true;
                comboLvText[i].enabled = true;
                comboNoText[i].enabled = true;
            }
            count = i + 1;
            Debug.Log("count" + count);
            comboLvText[i].text = "Lv ." + comboDatas[i].comboLevel.ToString();
        }
        comboNoText[count - 1].text = "(" + count + "/" + comboDatas.Length + ")";
    }
}
