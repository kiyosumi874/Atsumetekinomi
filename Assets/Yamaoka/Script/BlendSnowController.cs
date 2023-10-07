using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendSnowController : MonoBehaviour
{
    public float speed = 0.000001f;    // �Ⴊ�ς��鑬��
    private float amount;      // �ϐ��
    public float max = 1.0f;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        amount = 0.004f;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(amount <= 1f)
    //    {
    //        amount += speed;
    //        // �}�e���A�����擾���A�V�F�[�_�[�萔��ݒ�
    //        this.GetComponent<Renderer>().material.SetFloat("_Amount", amount);
    //    }
    //}

    private void FixedUpdate()
    {
        if(amount < max)
        {
            amount = amount + speed;
            mat.SetFloat("_Amount", amount);
        }
    }
}
