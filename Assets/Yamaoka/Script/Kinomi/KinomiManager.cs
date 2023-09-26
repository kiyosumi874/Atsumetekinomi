using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 木の実データ
/// </summary>
public class KinomiData
{
    public int id;      // 木の実ID
    private int count;  // 所持数

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public KinomiData(int id, int count = 1)
    {
        this.id = id;
        this.count = count;
    }

    /// <summary>
    /// 所持数カウントアップ
    /// </summary>
    /// <param name="value"></param>
    public void CountUp(int value = 1)
    {
        count += value;
    }
    /// <summary>
    /// 所持数カウントダウン
    /// </summary>
    /// <param name="value"></param>
    public void CountDown(int value = 1)
    {
        count -= count;
    }
}

/// <summary>
/// 木の実管理クラス
/// </summary>
public class KinomiManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiSourceData> kinomiSourceDataList;    // 木の実ソースリスト

    private List<KinomiData> playerKinomiDataList = new List<KinomiData>();     // プレイヤーの所持木の実

    /// <summary>
    /// 木の実データをロードする
    /// </summary>
    private void LoadKinomiSourceData()
    {
        kinomiSourceDataList =
            Resources.LoadAll("ScritableObject", typeof(KinomiData)).Cast<KinomiSourceData>().ToList();
    }

    /// <summary>
    /// 木の実ソースデータを取得
    /// </summary>
    /// <param name="id">木の実ID</param>
    /// <returns>該当する木の実のソースデータ</returns>
    public KinomiSourceData GetKinomiSourceData(int id)
    {
        // 木の実を検索
        foreach(var sourceData in kinomiSourceDataList)
        {
            // IDが一致していたら
            if(sourceData.kinomiID == id)
            {
                return sourceData;
            }
        }

        return null;
    }

    /// <summary>
    /// 木の実を取得
    /// </summary>
    /// <param name="kinomiID">木の実ID</param>
    /// <param name="count">追加する個数</param>
    public void CountItem(int kinomiID, int count)
    {
        for(int i = 0; i < playerKinomiDataList.Count; i++)
        {
            if (playerKinomiDataList[i].id == kinomiID)
            {
                playerKinomiDataList[i].CountUp(count);
                break;
            }
        }

        // IDが一致しなければ、木の実を追加
        KinomiData kinomiData = new KinomiData(kinomiID, count);
        playerKinomiDataList.Add(kinomiData);
    }
}
