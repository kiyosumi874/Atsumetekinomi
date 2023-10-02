using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R���{�̃f�[�^
/// </summary>
[System.Serializable]
public class ComboData
{
    public string comboName;    // �R���{��
    public int comboCount;      // �R���{�g�p��
    public int firstComboScore = 500;   // �ŏ��Ɏg�p�����Ƃ��̃X�R�A
    public int normalComboScore = 100;  // �ʏ펞�̃X�R�A
    public int comboLevel = 1;      // �R���{���x��
    public int comboLvUpCount = 5;
    public bool isFrist = true;     // ���߂Ďg�p���邩�ǂ���
    public bool useCombo = false;   // �R���{���g���邩�ǂ���
    public int useAppleNum = 0;
    public int useOrengeNum = 0;
    public int useBananaNum = 0;
    public int useRemonNum = 0;
    //public bool useApple;
    //public bool useOrenge;
    //public bool useBanana;
    //public bool useRemon;

    /// <summary>
    /// �R���{���g�p����
    /// </summary>
    /// <param name="_isFirst">�g�p����̂����߂Ă��ǂ���</param>
    /// <param name="score">�X�R�A</param>
    public int UseCombo(bool _isFirst, int score)
    {
        if(_isFirst)
        {
            score += firstComboScore;
        }
        else
        {
            score += normalComboScore * comboLevel;
        }
        comboCount++;

        Debug.Log(comboName + "������");
        Debug.Log("�X�R�A�F" + score);

        return score;
    }
}
