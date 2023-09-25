using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊んでいるときの処理担当
/// </summary>
public class PlayScene : MonoBehaviour
{
    // 遊んでいるときの処理段階
    static private PlaySceneState state = PlaySceneState.start;
    //何かボタンを押してからゲーム開始まで数える
    private Timer timer;
    //ゲーム開始までに数える時間
    private const float countDownNum = 5;
    //次のシーンの名前
    [SerializeField] private string nextSceneName;

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
    }
    /// <summary>
    /// ゲーム説明したらタイマー起動
    /// </summary>
    private void StartProcess()
    {
        if (timer == null) 
        {
            //なんか押されたら
            if (Input.anyKey)
            {
                timer = new Timer(countDownNum);
            } 
        }
        //カウントダウン終了時
        else if(timer.IsCountDownEnd())
        {
            state = PlaySceneState.play; 
        }
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
        SceneChanger.Instance.LoadSceneFaded(nextSceneName);
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