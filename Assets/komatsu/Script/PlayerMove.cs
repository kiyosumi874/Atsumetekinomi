using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�ϐ��錾
    private Rigidbody rigidbody;    //���W�b�h�{�f�B
    Vector3 direction_vector;   //�ړ������̃x�N�g��
    public const float MOVESPEED = 5.0f;   //�ړ����x
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������L�[�������ꂽ��ړ�
        Vector3 vector = new Vector3();
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.x = 1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.x = -1;
        }
        //z���̈ړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.z = -1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.z = 1;
        }

        //���K�����Ĉړ��ʂ�������
        vector.Normalize();
        direction_vector = vector * MOVESPEED;
        rigidbody.velocity = direction_vector;
    }
}
