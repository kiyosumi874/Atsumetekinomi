using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V��ł���Ƃ��̏����S��
/// </summary>
public class PlayScene : MonoBehaviour
{
    // �V��ł���Ƃ��̏����i�K
    static private PlaySceneState state = PlaySceneState.start;
    //�����{�^���������Ă���Q�[���J�n�܂Ő�����
    private Timer timer;
    //�Q�[���J�n�܂łɐ����鎞��
    private const float countDownNum = 5;
    //���̃V�[���̖��O
    [SerializeField] private string nextSceneName;

    /// <summary>
    /// �i�K�ɂ���ď����ύX
    /// </summary>
    void Update()
    {
        switch (state)
        {
            case PlaySceneState.start:
                StartProcess();
                break;
            case PlaySceneState.play:
                PlayGameProcess();
                break;
            case PlaySceneState.end:
                EndProcess();
                break;   
        }
    }
    /// <summary>
    /// �Q�[������������^�C�}�[�N��
    /// </summary>
    private void StartProcess()
    {
        if (timer == null) 
        {
            //�Ȃ񂩉����ꂽ��
            if (Input.anyKey)
            {
                timer = new Timer(countDownNum);
            } 
        }
        //�J�E���g�_�E���I����
        else if(timer.IsCountDownEnd())
        {
            state = PlaySceneState.play; 
        }
    }
    /// <summary>
    /// �V��ł���Ƃ��̏���
    /// </summary>
    private void PlayGameProcess()
    {
        //�������Ԍo��
        if (PlayLimitTimer.IsOverLimitTime())
        {
            state = PlaySceneState.end;
        }
    }
    /// <summary>
    /// �I�������@�V�[���J�ڂ���
    /// </summary>
    private void EndProcess()
    {
        SceneChanger.Instance.LoadSceneFaded(nextSceneName);
    }
    /// <summary>
    /// �����̏��������Ă��邩
    /// </summary>
    /// <returns></returns>
    static public PlaySceneState GetNowProcess()
    {
        return state;
    }
}