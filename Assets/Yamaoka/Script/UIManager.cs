using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

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

    public Text countDownText;
    public Text gameTimeText;

    //���ׂĂ̖؂̎��̉摜���i�[
    public List<UnityEngine.UI.Image> appleImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> lemonImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> orangeImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> bananaImgs = new List<UnityEngine.UI.Image>();
    public List<UnityEngine.UI.Image> watermelonImgs = new List<UnityEngine.UI.Image>();

    // ����p�l��
    public GameObject operationPanel;

    private void Awake()
    {
        instance = this;
        //InitComboUI();
        countDownText.enabled = false;
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
                //if (comboDatas[i].comboName == data.comboName
                //    && !comboDatas[i].isFrist)
                //{
                //    // �g�p���ꂽ�R���{��UI��\������
                //    data.comboUI.SetActive(true);
                //}

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

    /// <summary>
    /// �R���{UI�̊g��E�k���A�j���[�V����
    /// </summary>
    /// <param name="comboDatas">�R���{�f�[�^</param>
    public void ChangeComboUIScale(ComboData[] comboDatas)
    {
        foreach (ComboUIData data in comboUIDataList)
        {
            RectTransform rectTransform;
            rectTransform = data.comboUI.GetComponent<RectTransform>();
            var startScale = new Vector3(0.13f, 0.13f, 1);
            var endScale = new Vector3(0.14f, 0.14f, 1);
            for (int i = 0; i < comboDatas.Length; i++)
            {
                if (comboDatas[i].useCombo
                    && comboDatas[i].comboName == data.comboName
                    && !comboDatas[i].isFrist)
                {
                    if (rectTransform.localScale.x <= startScale.x)
                    {
                        rectTransform.DOScale(
                            endScale,
                            1.0f);
                    }
                    if (rectTransform.localScale.x >= endScale.x)
                    {
                        rectTransform.DOScale(
                            startScale,
                            1.0f);
                    }
                }
                else if(!comboDatas[i].useCombo
                    && comboDatas[i].comboName == data.comboName)
                {
                    data.comboUI.GetComponent<RectTransform>().localScale = startScale;
                }
            }

        }
    }

    /// <summary>
    /// �؂̎��摜�̓��ߓx��ݒ�
    /// </summary>
    /// <param name="apple">hasApple</param>
    /// <param name="lemon">hasLemon</param>
    /// <param name="orange">hasOrange</param>
    /// <param name="banana">hasBanana</param>
    /// <param name="watermelon">hasWatermelon</param>
    public void ChangeKinomiImgAlpha(bool apple, bool lemon, bool orange, bool banana, bool watermelon)
    {
        Color hasNotKinomi = new Color(1, 1, 1, 0.8f);  // �؂̎��������Ă��Ȃ��Ƃ��̓��ߓx
        Color hasKinomi = new Color(1, 1, 1, 1.0f);     // �؂̎��������Ă���Ƃ��̓��ߓx
        // �����S�摜�̐F��ݒ�
        for (int i = 0; i < appleImgs.Count; i++)
        {
            if(apple)
            {
                appleImgs[i].color = hasKinomi;
            }
            else
            {
                appleImgs[i].color = hasNotKinomi;
            }
        }
        // �������摜�̐F��ݒ�
        for (int i = 0; i < lemonImgs.Count; i++)
        {
            if (lemon)
            {
                lemonImgs[i].color = hasKinomi;
            }
            else
            {
                lemonImgs[i].color = hasNotKinomi;
            }
        }
        // �I�����W�摜�̐F��ݒ�
        for (int i = 0; i < orangeImgs.Count; i++)
        {
            if (orange)
            {
                orangeImgs[i].color = hasKinomi;
            }
            else
            {
                orangeImgs[i].color = hasNotKinomi;
            }
        }
        // �o�i�i�摜�̐F��ݒ�
        for (int i = 0; i < bananaImgs.Count; i++)
        {
            if (banana)
            {
                bananaImgs[i].color = hasKinomi;
            }
            else
            {
                bananaImgs[i].color = hasNotKinomi;
            }
        }
        // �X�C�J�摜�̐F��ݒ�
        for (int i = 0; i < watermelonImgs.Count; i++)
        {
            if (watermelon)
            {
                watermelonImgs[i].color = hasKinomi;
            }
            else
            {
                watermelonImgs[i].color = hasNotKinomi;
            }
        }
    }
}
