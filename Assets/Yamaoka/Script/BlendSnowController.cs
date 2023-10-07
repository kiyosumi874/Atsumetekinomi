using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendSnowController : MonoBehaviour
{
    public float speed = 0.000001f;    // �Ⴊ�ς��鑬��
    private float amount;        // �ϐ��
    public float max = 1.0f;     // �ő�ϐ��
    public Material mat;         // �}�e���A�����擾

    // Start is called before the first frame update
    void Start()
    {
        // �ŏ����炠����x�ς�������Ԃ���n�߂�
        amount = 0.004f;
    }

    private void FixedUpdate()
    {
        if(amount < max)
        {
            // �ϐ�ʂ̌v�Z
            amount = amount + speed;
            // �V�F�[�_�[�萔�ݒ�
            mat.SetFloat("_Amount", amount);
        }
    }
}
