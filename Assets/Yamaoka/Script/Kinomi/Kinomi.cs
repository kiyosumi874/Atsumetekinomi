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
    public GenerationLocation generatLocation;  // �؂̎��̐����ꏊ

    public int score = 50;

    public static Kinomi instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            KinomiManager.instance.CountItem(kinomiName, 1);
            this.gameObject.SetActive(false);
        }
    }
}
