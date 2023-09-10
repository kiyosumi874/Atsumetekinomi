using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �؂̎������Ɉړ�����̂ɕK�v�ȃN���X
/// </summary>
public class KinomiMover : MonoBehaviour
{
    //��������ꏊ
    private Vector3 dropPoint= new Vector3(0,0,0);
    private Vector3 firstPos= new Vector3(0,0,0);
    //�ړ�����̂ɕK�v�Ȏ���
    private float moveTime = 10.0f;
    //�ړ���������
    private float movedTime = 0;
    //�ړ��I���t���O
    private bool isEndMove = false;

    private void Start()
    {
        firstPos= transform.position;
    }
    private void FixedUpdate()
    {
        if (!isEndMove)
        {
            //�ړ��������o��
            float lerpValue = movedTime / moveTime;
            if (lerpValue < 1)
            {
                //�ړ�����
                transform.position = Vector3.Lerp(firstPos, dropPoint, lerpValue);
            }
            //�ړ����Ԃ��o�߂�����
            movedTime += Time.deltaTime;
            //�ړ��I��
            if (movedTime > moveTime)
            {
                transform.position = dropPoint;
                isEndMove = true;
            }
        }
    }
}
