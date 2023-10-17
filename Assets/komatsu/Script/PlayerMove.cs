using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    //�ϐ��錾

    //�ړ��֘A
    private Rigidbody rigidbody;                 //���W�b�h�{�f�B
    private Vector3 direction_vector;            //�ړ������̃x�N�g��
    public const float MOVESPEED = 5.0f;         //�ړ����x

    //�A�j���[�V�����֘A
    private Animator animator;                  //�A�j���[�^�[
    private bool isMove;                        //�������Ă��邩�ǂ���

    //�p�x�֘A
    Vector3 prev_position;                      //�O�t���[���̍��W
    public const float ROTATE_SPEED = 10.0f;    //��]���鑬��
    

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
        //�ړ��ʂ��v�Z����
        Vector3 current_potision = transform.position;
        Vector3 delta_movement = current_potision - prev_position;
        prev_position = current_potision;
        //�ړ����Ȃ班������]������
        if (isMove)
        {
            transform.forward = Vector3.Slerp(transform.forward, delta_movement, Time.deltaTime * ROTATE_SPEED);
        }
    }
}
