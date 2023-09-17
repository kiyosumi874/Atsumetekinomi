using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊んでいるときの処理担当
/// </summary>
public class PlayScene : MonoBehaviour
{
    /// <summary>
    /// 遊んでいるときの処理段階
    /// </summary>
    static private PlaySceneState state = PlaySceneState.start;
    /// <summary>
    /// 段階によって処理変更
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
    /// ゲーム説明したらタイマー起動
    /// </summary>
    private void StartProcess()
    {
        state = PlaySceneState.play;
    }
    /// <summary>
    /// 遊んでいるときの処理
    /// </summary>
    private void PlayGameProcess()
    {
        //制限時間経過
        if (PlayLimitTimer.IsOverLimitTime())
        {
            state = PlaySceneState.end;
        }
    }
    /// <summary>
    /// 終了処理　シーン遷移する
    /// </summary>
    private void EndProcess()
    {
    }
    /// <summary>
    /// 今何の処理をしているか
    /// </summary>
    /// <returns></returns>
    static public PlaySceneState GetNowProcess()
    {
        return state;
    }
}
