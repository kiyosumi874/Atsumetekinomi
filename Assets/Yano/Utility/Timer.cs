using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 時間を計るクラス
/// </summary>
public class Timer
{
    //計測開始時間
    private float startTime;
    //時間経過の基準
    private float limitTime;
    //カウントダウン終了フラグ
    private bool countDownEnd = false;
    /// <summary>
    /// 引数の時間経っているか調べるクラス
    /// </summary>
    /// <param name="setLimitTime"></param>
    public Timer(float setLimitTime)
    {
        limitTime = setLimitTime;
        startTime = Time.time;
    }
    /// <summary>
    /// 残り時間
    /// </summary>
    /// <returns>残り時間</returns>
    public float GetRemainingTime()
    {
        if(IsCountDownEnd())
        {
            return 0;
        }
        return limitTime - (Time.time - startTime) ;
    }
    /// <summary>
    /// カウントダウン終了
    /// </summary>
    /// <returns>コンストラクタで設定した時間を超過したらTrue</returns>
    public bool IsCountDownEnd()
    {
        return Time.time - startTime > limitTime;
    }
}
