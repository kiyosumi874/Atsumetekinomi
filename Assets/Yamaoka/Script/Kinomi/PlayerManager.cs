using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiData> kinomiDataList = new List<KinomiData>();   // �v���C���[�̏����؂̎�

    /// <summary>
    /// �؂̎����擾
    /// </summary>
    /// <param name="kinomiID">�؂̎���ID</param>
    /// <param name="count">�擾�����</param>
    public void CountKinomi(string kinomiName, int count)
    {
        // List��������
        for(int i = 0; i < kinomiDataList.Count; i++)
        {
            // ID����v���Ă�����J�E���g
            if (kinomiDataList[i].name == kinomiName)
            {
                kinomiDataList[i].CountUp(count);
                break;
            }
        }

        // ID����v���Ȃ���΃A�C�e����ǉ�
        KinomiData kinomiData = new KinomiData(kinomiName, count);
        kinomiDataList.Add(kinomiData);
    }

    /// <summary>
    /// �؂̎������X�g
    /// </summary>
    /// <param name="kinomiID">�؂̎���ID</param>
    /// <param name="count">���X�g�����</param>
    public void LostKinomi(int kinomiID, int count)
    {
        // List��������
        for (int i = 0; i < kinomiDataList.Count; i++)
        {
            // ID����v���Ă�����J�E���g
            if (kinomiDataList[i].id == kinomiID)
            {
                kinomiDataList[i].CountDown(count);
                break;
            }
        }
    }

}
