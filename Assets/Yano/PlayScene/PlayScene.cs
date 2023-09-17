using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V��ł���Ƃ��̏����S��
/// </summary>
public class PlayScene : MonoBehaviour
{
    /// <summary>
    /// �V��ł���Ƃ��̏����i�K
    /// </summary>
    static private PlaySceneState state = PlaySceneState.start;
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
        Debug.Log(state);
    }
    /// <summary>
    /// �Q�[������������^�C�}�[�N��
    /// </summary>
    private void StartProcess()
    {
        state = PlaySceneState.play;
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
