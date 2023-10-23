using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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
        UpdateComboLevel();
        UIManager.instance.ShowComboUI(comboDatas);
        //UIManager.instance.ChangeComboUIScale(comboDatas);
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
            if (comboDatas[i].useCombo)
            {
                score =
                    comboDatas[i].UseCombo(comboDatas[i].isFrist, score);
                //SubtractionUseComboKinomi(comboDatas[i]);
                ComboEffectManager.instance.SetComboName(comboDatas[i]);
                ComboEffectManager.instance.SetFirstComboText(comboDatas[i]);
            }
        }
        GetNotComboScore();
    }

    /// <summary>
    /// �R���{�Ŏg�p���ꂽ�؂̎������Z����
    /// </summary>
    public void SubtractionUseComboKinomi(ComboData comboData)
    {
        switch(comboData.comboName)
        {
            case "�����S�I�����W":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                break;
            case "�I�����W�o�i�i":
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                break;
            case "�����S������":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                break;
            case "�������o�i�i":
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                break;
            case "�܂邫�̂�":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.watermelonNum -= comboData.useWatermelonNum;
                break;
            case "�����":
                KinomiManager.instance.appleNum -= comboData.useAppleNum;
                KinomiManager.instance.orengeNum -= comboData.useOrengeNum;
                KinomiManager.instance.bananaNum -= comboData.useBananaNum;
                KinomiManager.instance.lemonNum -= comboData.useLemonNum;
                KinomiManager.instance.watermelonNum -= comboData.useWatermelonNum;
                break;
        }
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
                kinomiScore += KinomiManager.instance.appleScore * KinomiManager.instance.appleNum;
                KinomiManager.instance.appleNum = 0;
            }
            if (KinomiManager.instance.orengeNum >= 1)
            {
                kinomiScore += KinomiManager.instance.orengeScore * KinomiManager.instance.orengeNum;
                KinomiManager.instance.orengeNum = 0;
            }
            if (KinomiManager.instance.bananaNum >= 1 )
            {
                kinomiScore += KinomiManager.instance.bananaScore * KinomiManager.instance.bananaNum;
                KinomiManager.instance.bananaNum = 0;
            }
            if(KinomiManager.instance.lemonNum >= 1)
            {
                kinomiScore += KinomiManager.instance.lemonScore * KinomiManager.instance.lemonNum;
                KinomiManager.instance.lemonNum = 0;
            }
            if (KinomiManager.instance.watermelonNum >= 1)
            {
                kinomiScore += KinomiManager.instance.watermelonScore * KinomiManager.instance.watermelonNum;
                KinomiManager.instance.watermelonNum = 0;
            }
        }
        // �g����R���{������A���؂̎����]���Ă���ꍇ
        else if (comboDataList.Count > 0
            || KinomiManager.instance.appleNum >= 1
            || KinomiManager.instance.orengeNum >= 1
            || KinomiManager.instance.bananaNum >= 1
            || KinomiManager.instance.lemonNum >= 1
            || KinomiManager.instance.watermelonNum >= 1)
        {
            kinomiScore += OverKinomiScore();
        }
        Debug.Log("�擾�X�R�A(�؂̎��̂�)�F" + kinomiScore);
        // ���݂̃X�R�A�ɉ��Z
        score += kinomiScore;
        ComboEffectManager.instance.SetNoComboEffect(kinomiScore);
        Debug.Log("���v�X�R�A�F" + score);
    }

    /// <summary>
    /// �R���{�Ŏg�p���ꂸ�ɗ]�����؂̎��̃X�R�A���v�Z
    /// </summary>
    /// <param name="score">���݂̖؂̎��X�R�A</param>
    /// <param name="comboData">�R���{�f�[�^</param>
    /// <returns>�v�Z���ꂽ�؂̎��X�R�A</returns>
    public int OverKinomiScore(/*ComboData comboData*/)
    {
        int score = 0;
        //if (KinomiManager.instance.appleNum >= comboData.useAppleNum
        //    && KinomiManager.instance.hasApple)
        if (KinomiManager.instance.appleNum >= 1)
        {
            score += KinomiManager.instance.appleScore * (KinomiManager.instance.appleNum - 1);
            KinomiManager.instance.appleNum = 0;
            //Debug.Log("AppleNum : " + KinomiManager.instance.appleNum);
            //Debug.Log("Apple : " + score);
        }
        //if (KinomiManager.instance.orengeNum >= comboData.useOrengeNum
        //    && KinomiManager.instance.hasOrenge)
        if (KinomiManager.instance.orengeNum >= 1)
        {
            score += KinomiManager.instance.orengeScore * (KinomiManager.instance.orengeNum - 1);
            KinomiManager.instance.orengeNum = 0;
            //Debug.Log("OrengeNum : " + KinomiManager.instance.orengeNum);
            //Debug.Log("Orange : " + score);
        }
        //if (KinomiManager.instance.bananaNum >= comboData.useBananaNum
        //    && KinomiManager.instance.hasBanana)
        if (KinomiManager.instance.bananaNum >= 1)
        {
            score += KinomiManager.instance.bananaScore * (KinomiManager.instance.bananaNum - 1);
            KinomiManager.instance.bananaNum = 0;
            //Debug.Log("BananaNum : " + KinomiManager.instance.bananaNum);
            //Debug.Log("Banana : " + score);
        }
        //if (KinomiManager.instance.lemonNum >= comboData.useLemonNum
        //    && KinomiManager.instance.hasLemon)
        if (KinomiManager.instance.lemonNum >= 1)
        {
            score += KinomiManager.instance.lemonScore * (KinomiManager.instance.lemonNum - 1);
            KinomiManager.instance.lemonNum = 0;
            //Debug.Log("LemonNum : " + KinomiManager.instance.lemonNum);
            //Debug.Log("Lemon : " + score);
        }
        //if (KinomiManager.instance.watermelonNum >= comboData.useWatermelonNum
        //    && KinomiManager.instance.hasWatermelon)
        if (KinomiManager.instance.watermelonNum >= 1)
        {
            score += KinomiManager.instance.watermelonScore * (KinomiManager.instance.watermelonNum - 1);
            KinomiManager.instance.watermelonNum = 0;
            //Debug.Log("WatermelonNum : " + KinomiManager.instance.watermelonNum);
            //Debug.Log("Watermelon : " + score);
        }
        //ComboEffectManager.instance.SetNoComboEffect(score);
        Debug.Log("OverKinomiScore : " + score);
        return score;
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
            if (KinomiManager.instance.hasApple
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


            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasLemon)
            {
                if (comboDatas[i].comboName == "�����S������")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�����S������")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasBanana
                && KinomiManager.instance.hasLemon)
            {
                if (comboDatas[i].comboName == "�������o�i�i")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�������o�i�i")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge
                && KinomiManager.instance.hasWatermelon)
            {
                if (comboDatas[i].comboName == "�܂邫�̂�")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�܂邫�̂�")
                {
                    comboDatas[i].useCombo = false;
                }
            }

            if (KinomiManager.instance.hasApple
                && KinomiManager.instance.hasOrenge
                && KinomiManager.instance.hasBanana
                && KinomiManager.instance.hasLemon
                && KinomiManager.instance.hasWatermelon)
            {
                if (comboDatas[i].comboName == "�����")
                {
                    comboDatas[i].useCombo = true;
                }
            }
            else
            {
                if (comboDatas[i].comboName == "�����")
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
}
