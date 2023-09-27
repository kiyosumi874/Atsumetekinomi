using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// �؂̎��f�[�^
/// </summary>
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
    
    public int nowKinomiNum = 0;    // �������Ă���؂̎��̍��v��
    // ���ꂼ��̖؂̎��̌�
    public int appleNum = 0;
    public int orengeNum = 0;
    public int bananaNum = 0;

    public static KinomiManager instance;

    private void Awake()
    {
        LoadKinomiSourceData();
        instance = this;
    }

    private void Update()
    {
        allKinomiNumText.text = nowKinomiNum.ToString();
        appleNumText.text = appleNum.ToString();
        orengeNumText.text = orengeNum.ToString();
        bananaNumText.text = bananaNum.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            LostAllKinomi();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            LostKinomi("�����S", 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LostKinomi("�I�����W", 1);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LostKinomi("�o�i�i", 1);
        }
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

        Debug.Log(kinomiData.name + "���擾");
        Debug.Log("�����Ă���؂̎��̐��F" + playerKinomiDataList.Count);
        nowKinomiNum++;
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
            playerKinomiDataList.RemoveRange(0, nowKinomiNum);
        }
        nowKinomiNum = 0;
        appleNum = 0;
        orengeNum = 0;
        bananaNum = 0;

        Debug.Log("���ׂĂ̖؂̎������X�g���܂���");
        Debug.Log("�����Ă���؂̎��̐��F" + playerKinomiDataList.Count);
    }
}
