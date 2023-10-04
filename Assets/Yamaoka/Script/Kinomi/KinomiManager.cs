using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// �؂̎��f�[�^
/// </summary>
[System.Serializable]
public class KinomiData
{
    public int id;      // �؂̎�ID
    public string name; // �؂̎��̖��O
    public int count;   // ������

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public KinomiData(string name, int count = 1)
    {
        this.name = name;
        this.count = count;
    }

    /// <summary>
    /// �������J�E���g�A�b�v
    /// </summary>
    /// <param name="value"></param>
    public void CountUp(int value = 1)
    {
        count += value;
    }
    /// <summary>
    /// �������J�E���g�_�E��
    /// </summary>
    /// <param name="value"></param>
    public void CountDown(int value = 1)
    {
        count -= value;
    }
}

/// <summary>
/// �؂̎��Ǘ��N���X
/// </summary>
public class KinomiManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiSourceData> kinomiSourceDataList;    // �؂̎��\�[�X���X�g
    [SerializeField]
    private List<KinomiData> playerKinomiDataList = new List<KinomiData>();     // �v���C���[�̏����؂̎�

    [SerializeField]
    private KinomiData kinomiData;    // �؂̎��f�[�^

    public Text allKinomiNumText;
    public Text appleNumText;
    public Text orengeNumText;
    public Text bananaNumText;
    public Text lemonNumText;
    public Text watermelonNumText;

    public int nowKinomiNum = 0;    // �������Ă���؂̎��̍��v��
    public int maxKinomiNum = 10;   // �����ł���؂̎��̍ő吔
    // ���ꂼ��̖؂̎��̌�
    public int appleNum = 0;
    public int orengeNum = 0;
    public int bananaNum = 0;
    public int lemonNum = 0;
    public int watermelonNum = 0;
    // �؂̎���������t���O
    public bool hasApple = false;
    public bool hasOrenge = false;
    public bool hasBanana = false;
    public bool hasLemon = false;
    public bool hasWatermelon = false;

    public static KinomiManager instance;   // �C���X�^���X

    private void Awake()
    {
        LoadKinomiSourceData();
        instance = this;
    }

    private void Update()
    {
        CheckHasKinomi();
        SetMinAllKinomisNum();
        allKinomiNumText.text = nowKinomiNum.ToString();
        appleNumText.text = appleNum.ToString();
        orengeNumText.text = orengeNum.ToString();
        bananaNumText.text = bananaNum.ToString();
        lemonNumText.text = lemonNum.ToString();
        watermelonNumText.text = watermelonNum.ToString();
    }

    /// <summary>
    /// �؂̎��f�[�^�����[�h����
    /// </summary>
    private void LoadKinomiSourceData()
    {
        kinomiSourceDataList =
            Resources.LoadAll("ScritableObject", typeof(KinomiData)).Cast<KinomiSourceData>().ToList();
    }

    /// <summary>
    /// �؂̎��\�[�X�f�[�^���擾
    /// </summary>
    /// <param name="id">�؂̎�ID</param>
    /// <returns>�Y������؂̎��̃\�[�X�f�[�^</returns>
    public KinomiSourceData GetKinomiSourceData(int id)
    {
        // �؂̎�������
        foreach(var sourceData in kinomiSourceDataList)
        {
            // ID����v���Ă�����
            if(sourceData.kinomiID == id)
            {
                return sourceData;
            }
        }

        return null;
    }

    /// <summary>
    /// �؂̎��������Ă��邩�ǂ�������
    /// </summary>
    public void CheckHasKinomi()
    {
        if(appleNum <= 0)
        {
            hasApple = false;
        }
        else
        {
            hasApple = true;
        }
        if(orengeNum <= 0)
        {
            hasOrenge = false;
        }
        else
        {
            hasOrenge = true;
        }
        if(bananaNum <= 0)
        {
            hasBanana = false;
        }
        else
        {
            hasBanana = true;
        }
        if (lemonNum <= 0)
        {
            hasLemon = false;
        }
        else
        {
            hasLemon = true;
        }
        if (watermelonNum <= 0)
        {
            hasWatermelon = false;
        }
        else
        {
            hasWatermelon = true;
        }
    }

    /// <summary>
    /// �؂̎����擾
    /// </summary>
    /// <param name="kinomiID">�؂̎�ID</param>
    /// <param name="count">�ǉ������</param>
    public void CountItem(string kinomiName, int count)
    {
        // List��������
        foreach (KinomiData data in playerKinomiDataList)
        {
            if (data.name == kinomiName)
            {
                data.CountUp(count);
                break;
            }
        }

        // ID����v���Ȃ���΁A�؂̎���ǉ�
        kinomiData = new KinomiData(kinomiName, count);
        playerKinomiDataList.Add(kinomiData);

        if(nowKinomiNum < maxKinomiNum)
        {
            if (kinomiName == "�����S")
            {
                appleNum++;
            }
            else if (kinomiName == "�I�����W")
            {
                orengeNum++;
            }
            else if (kinomiName == "�o�i�i")
            {
                bananaNum++;
            }
            else if (kinomiName == "������")
            {
                lemonNum++;
            }
            else if (kinomiName == "�X�C�J")
            {
                watermelonNum++;
            }

            nowKinomiNum++;
        }

        if(nowKinomiNum >= maxKinomiNum)
        {
            nowKinomiNum = maxKinomiNum;
        }

        Debug.Log(kinomiData.name + "���擾");
        Debug.Log("�����Ă���؂̎��̐��F" + playerKinomiDataList.Count);
    }

    /// <summary>
    /// �؂̎������X�g
    /// </summary>
    /// <param name="kinomiID">�؂̎���ID</param>
    /// <param name="count">���X�g�����</param>
    public void LostKinomi(string kinomiName, int count)
    {
        // List��������
        foreach (KinomiData data in playerKinomiDataList)
        {
            if(data.name == kinomiName)
            {
                data.CountDown(count);

                playerKinomiDataList.Remove(data);

                if (kinomiName == "�����S")
                {
                    appleNum -= count;
                }
                else if (kinomiName == "�I�����W")
                {
                    orengeNum -= count;
                }
                else if (kinomiName == "�o�i�i")
                {
                    bananaNum -= count;
                }
                else if (kinomiName == "������")
                {
                    lemonNum -= count;
                }
                else if (kinomiName == "�X�C�J")
                {
                    watermelonNum -= count;
                }
                nowKinomiNum -= count;
                Debug.Log(data.name + "�� " + count + "���X�g");
                Debug.Log("�����Ă���؂̎��̐��F" + playerKinomiDataList.Count);
                break;
            }
            else
            {
                Debug.Log(kinomiName + "���������Ă��܂���");
            }
        }
    }

    /// <summary>
    /// ���ׂĂ̖؂̎������X�g
    /// </summary>
    public void LostAllKinomi()
    {
        // List��������
        for (int i = 0; i < playerKinomiDataList.Count; i++)
        {
            playerKinomiDataList.RemoveRange(0, i);
        }
        nowKinomiNum = 0;
        appleNum = 0;
        orengeNum = 0;
        bananaNum = 0;
        lemonNum = 0;
        watermelonNum = 0;
        Debug.Log("���ׂĂ̖؂̎������X�g���܂���");
        Debug.Log("�����Ă���؂̎��̐��F" + playerKinomiDataList.Count);
    }

    /// <summary>
    /// ���ׂĂ̖؂̎��̏������̍ŏ��l��ݒ�
    /// </summary>
    public void SetMinAllKinomisNum()
    {
        if(appleNum <= 0)
        {
            appleNum = 0;
        }
        if (orengeNum <= 0)
        {
            orengeNum = 0;
        }
        if (bananaNum <= 0)
        {
            bananaNum = 0;
        }
        if (lemonNum <= 0)
        {
            lemonNum = 0;
        }
        if (watermelonNum <= 0)
        {
            watermelonNum = 0;
        }
    }
}
