using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �؂̎��N���X
/// </summary>
public class Kinomi : MonoBehaviour
{
    /// <summary>
    /// �؂̎��̐����ꏊ�̃f�[�^
    /// </summary>
    public enum GenerationLocation
    {
        Near,    // ���̋߂�
        Far,     // �����牓��
        Middle   // ����
    }

    [SerializeField]
    private string kinomiName;  // �؂̎��̖��O
    [SerializeField]
    GenerationLocation generatLocation;  // �؂̎��̐����ꏊ


    [SerializeField]
    private KinomiManager kinomiManager;

    public void OnCollisionEnter(Collision collision)
    {
        kinomiManager.CountItem(kinomiName, 1);
        this.gameObject.SetActive(false);
    }
}
