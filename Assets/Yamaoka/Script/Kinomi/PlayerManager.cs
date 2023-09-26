using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiData> kinomiDataList = new List<KinomiData>();   // プレイヤーの所持木の実

    /// <summary>
    /// 木の実を取得
    /// </summary>
    /// <param name="kinomiID">木の実のID</param>
    /// <param name="count">取得する個数</param>
    public void CountKinomi(string kinomiName, int count)
    {
        // List内を検索
        for(int i = 0; i < kinomiDataList.Count; i++)
        {
            // IDが一致していたらカウント
            if (kinomiDataList[i].name == kinomiName)
            {
                kinomiDataList[i].CountUp(count);
                break;
            }
        }

        // IDが一致しなければアイテムを追加
        KinomiData kinomiData = new KinomiData(kinomiName, count);
        kinomiDataList.Add(kinomiData);
    }

    /// <summary>
    /// 木の実をロスト
    /// </summary>
    /// <param name="kinomiID">木の実のID</param>
    /// <param name="count">ロストする個数</param>
    public void LostKinomi(int kinomiID, int count)
    {
        // List内を検索
        for (int i = 0; i < kinomiDataList.Count; i++)
        {
            // IDが一致していたらカウント
            if (kinomiDataList[i].id == kinomiID)
            {
                kinomiDataList[i].CountDown(count);
                break;
            }
        }
    }

}
