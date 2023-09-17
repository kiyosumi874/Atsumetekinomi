using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 制限時間計測係
/// </summary>
public class PlayLimitTimer : MonoBehaviour
{
    /// <summary>
    /// 表示するテキスト
    /// </summary>
    [SerializeField] private Text timerText;
    /// <summary>
    /// 制限時間とかノルマとかの設定
    /// </summary>
    [SerializeField] private PlaySceneOption option;
    /// <summary>
    /// タイマー計測開始時間
    /// </summary>
    private float startTime = 0;
    /// <summary>
    /// 制限時間超過フラグ
    /// </summary>
    private static bool isOverLimitTime = false;
    /// <summary>
    /// static変数を初期化
    /// </summary>
    private void Start()
    {
        isOverLimitTime = false;
    }
    /// <summary>
    /// 制限時間を更新
    /// </summary>
    private void Update()
    {
        if (PlayScene.GetNowProcess() == PlaySceneState.play)//遊んでいるときは残り時間表示
        {
            isOverLimitTime = GetElaspedTime() > option.getPlayLimitTime;//制限時間を超過しているか調べる
            //残り時間
            int remainingTime = ((int)(option.getPlayLimitTime - GetElaspedTime()));
            if(isOverLimitTime)//制限時間過ぎたら0に
            {
                remainingTime = 0;
            }
            //UI変更
            timerText.gameObject.SetActive(true);
            timerText.text = remainingTime.ToString();
        }
        else//遊んでいない時は隠す
        {
            timerText.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 経過時間所得
    /// </summary>
    /// <returns>経過時間</returns>
    public float GetElaspedTime()
    { 
        return Time.time - startTime; 
    }
    /// <summary>
    /// タイマーが制限時間を過ぎたか調べる
    /// </summary>
    /// <returns>経過したらTrue</returns>
    public static bool IsOverLimitTime()
    {
        return isOverLimitTime;
    }
}