using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    //�ϐ��錾

    //�ړ��֘A
    private Rigidbody rigidbody;            //���W�b�h�{�f�B
    private Vector3 direction_vector;       //�ړ������̃x�N�g��
    public const float MOVESPEED = 5.0f;   //�ړ����x

    //�A�j���[�V�����֘A
    private Animator animator;  //�A�j���[�^�[
    private bool isMove;        //�������Ă��邩�ǂ���

    //�p�x�֘A
    float round = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ��֘A������
        Vector3 vector = new Vector3();
        isMove = false;

        //�������L�[�������ꂽ��ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.x = -1;
            isMove = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.x = 1;
            isMove = true;
        }
        //z���̈ړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.z = 1;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //�x�N�g���Ɉړ�������������
            vector.z = -1;
            isMove = true;
        }

        //���K�����Ĉړ��ʂ�������
        vector.Normalize();
        direction_vector = vector * MOVESPEED;
        rigidbody.velocity = direction_vector;

        //�A�j���[�V��������
        if(isMove)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        //�p�x����
        //TODO�F�������ɓ��͂���Ɖ�]����肭�����Ȃ��B�C�����邱��
        //Transform transform = this.transform;
        //Vector3 local_angle = transform.localEulerAngles;
        ////�����Ă���Ȃ��]�����v�Z
        //if(isMove)
        //{
        //    round = Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
        //}
        
        //Debug.Log(round);
        //if(local_angle.y > round)
        //{
        //    local_angle.y -= 1.0f;
        //}
        //else if(local_angle.y < round)
        //{
        //    local_angle.y += 1.0f;
        //}
        //transform.eulerAngles = local_angle;
    }
}
