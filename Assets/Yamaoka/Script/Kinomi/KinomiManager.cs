using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �؂̎��f�[�^
/// </summary>
public class KinomiData
{
    public int id;      // �؂̎�ID
    private int count;  // ������

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public KinomiData(int id, int count = 1)
    {
        this.id = id;
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
        count -= count;
    }
}

/// <summary>
/// �؂̎��Ǘ��N���X
/// </summary>
public class KinomiManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiSourceData> kinomiSourceDataList;    // �؂̎��\�[�X���X�g

    private List<KinomiData> playerKinomiDataList = new List<KinomiData>();     // �v���C���[�̏����؂̎�

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
    public void CountItem(int kinomiID, int count)
    {
        for(int i = 0; i < playerKinomiDataList.Count; i++)
        {
            if (playerKinomiDataList[i].id == kinomiID)
            {
                playerKinomiDataList[i].CountUp(count);
                break;
            }
        }

        // ID����v���Ȃ���΁A�؂̎���ǉ�
        KinomiData kinomiData = new KinomiData(kinomiID, count);
        playerKinomiDataList.Add(kinomiData);
    }
}
