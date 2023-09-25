using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ԃ��v��N���X
/// </summary>
public class Timer
{
    //�v���J�n����
    private float startTime;
    //���Ԍo�߂̊
    private float limitTime;
    //�J�E���g�_�E���I���t���O
    private bool countDownEnd = false;
    /// <summary>
    /// �����̎��Ԍo���Ă��邩���ׂ�N���X
    /// </summary>
    /// <param name="setLimitTime"></param>
    public Timer(float setLimitTime)
    {
        limitTime = setLimitTime;
        startTime = Time.time;
    }
    /// <summary>
    /// �c�莞��
    /// </summary>
    /// <returns>�c�莞��</returns>
    public float GetRemainingTime()
    {
        if(IsCountDownEnd())
        {
            return 0;
        }
        return limitTime - (Time.time - startTime) ;
    }
    /// <summary>
    /// �J�E���g�_�E���I��
    /// </summary>
    /// <returns>�R���X�g���N�^�Őݒ肵�����Ԃ𒴉߂�����True</returns>
    public bool IsCountDownEnd()
    {
        return Time.time - startTime > limitTime;
    }
}
