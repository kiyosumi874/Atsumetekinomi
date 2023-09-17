using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 遊んでいるときの設定
/// </summary>
[CreateAssetMenu(menuName ="CreatePlaySceneOption")]
public class PlaySceneOption : ScriptableObject
{
    [SerializeField] [Header("ゲーム終了までの時間")] private float playLimitTime;
    [SerializeField] [Header("スコアのノルマ")] private float scoreQuota;
    /// <summary>
    /// 制限時間
    /// </summary>
    public float getPlayLimitTime { get { return playLimitTime; } private set { } }
    /// <summary>
    /// ゲームクリアに必要なスコア量
    /// </summary>
    public float getScoreQuota { get { return scoreQuota; } private set { } }
}
