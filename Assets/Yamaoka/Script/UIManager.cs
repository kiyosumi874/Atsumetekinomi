using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �R���{UI�̃f�[�^
/// </summary>
[System.Serializable]
public class ComboUIData
{
    public string comboName;        // �R���{�̖��O
    public GameObject comboUI;      // UI�̃I�u�W�F�N�g
    public Text comboLvText;        // ���x����\������e�L�X�g
    public Text comboNoText;        // �R���{�ԍ���\������e�L�X�g
    public Text addScore;           // ���Z�����X�R�A��\������e�L�X�g
    public int comboUINomber = 0;   // �R���{UI�̔ԍ�(�ォ�牽�Ԗڂ�)
}

/// <summary>
/// UI�}�l�[�W���[
/// </summary>
public class UIManager : MonoBehaviour
{
    // �R���{UI�f�[�^���i�[
    public List<ComboUIData> comboUIDataList = new List<ComboUIData>();

    public static UIManager instance;   // �C���X�^���X

    private void Awake()
    {
        instance = this;
        InitComboUI();
    }

    /// <summary>
    /// �R���{UI�̏�����
    /// </summary>
    public void InitComboUI()
    {
        for(int i = 0; i < comboUIDataList.Count; i++)
        {
            comboUIDataList[i].comboUI.SetActive(false);
        }
    }

    /// <summary>
    /// �R���{UI��\��
    /// </summary>
    /// <param name="comboDatas"></param>
    public void ShowComboUI(ComboData[] comboDatas)
    {
        foreach (ComboUIData data in comboUIDataList)
        {
            for (int i = 0; i < comboDatas.Length; i++)
            {
                if (comboDatas[i].comboName == data.comboName
                    && !comboDatas[i].isFrist)
                {
                    // �g�p���ꂽ�R���{��UI��\������
                    data.comboUI.SetActive(true);
                }

                if(comboDatas[i].comboName == data.comboName)
                {
                    // �eUI�̐������X�V
                    data.comboLvText.text = "Lv ." + comboDatas[i].comboLevel.ToString();
                    data.addScore.text = "+" + (comboDatas[i].normalComboScore * comboDatas[i].comboLevel).ToString();
                    data.comboNoText.text = "(" + data.comboUINomber + "/" + comboDatas.Length + ")";
                }
            }
        }
    }
}
