using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �������Ԍv���W
/// </summary>
public class PlayLimitTimer : MonoBehaviour
{
    /// <summary>
    /// �\������e�L�X�g
    /// </summary>
    [SerializeField] private Text timerText;
    /// <summary>
    /// �������ԂƂ��m���}�Ƃ��̐ݒ�
    /// </summary>
    [SerializeField] private PlaySceneOption option;
    /// <summary>
    /// �^�C�}�[�v���J�n����
    /// </summary>
    private float startTime = 0;
    /// <summary>
    /// �������Ԓ��߃t���O
    /// </summary>
    private static bool isOverLimitTime = false;
    /// <summary>
    /// static�ϐ���������
    /// </summary>
    private void Start()
    {
        isOverLimitTime = false;
    }
    /// <summary>
    /// �������Ԃ��X�V
    /// </summary>
    private void Update()
    {
        if (PlayScene.GetNowProcess() == PlaySceneState.play)//�V��ł���Ƃ��͎c�莞�ԕ\��
        {
            isOverLimitTime = GetElaspedTime() > option.getPlayLimitTime;//�������Ԃ𒴉߂��Ă��邩���ׂ�
            //�c�莞��
            int remainingTime = ((int)(option.getPlayLimitTime - GetElaspedTime()));
            if(isOverLimitTime)//�������ԉ߂�����0��
            {
                remainingTime = 0;
            }
            //UI�ύX
            timerText.gameObject.SetActive(true);
            timerText.text = remainingTime.ToString();
        }
        else//�V��ł��Ȃ����͉B��
        {
            timerText.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// �o�ߎ��ԏ���
    /// </summary>
    /// <returns>�o�ߎ���</returns>
    public float GetElaspedTime()
    { 
        return Time.time - startTime; 
    }
    /// <summary>
    /// �^�C�}�[���������Ԃ��߂��������ׂ�
    /// </summary>
    /// <returns>�o�߂�����True</returns>
    public static bool IsOverLimitTime()
    {
        return isOverLimitTime;
    }
}