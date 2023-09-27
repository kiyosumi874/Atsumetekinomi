using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// 木の実データ
/// </summary>
public class KinomiData
{
    public int id;      // 木の実ID
    public string name; // 木の実の名前
    public int count;   // 所持数

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public KinomiData(string name, int count = 1)
    {
        this.name = name;
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
        count -= value;
    }
}

/// <summary>
/// 木の実管理クラス
/// </summary>
public class KinomiManager : MonoBehaviour
{
    [SerializeField]
    private List<KinomiSourceData> kinomiSourceDataList;    // 木の実ソースリスト
    [SerializeField]
    private List<KinomiData> playerKinomiDataList = new List<KinomiData>();     // プレイヤーの所持木の実

    [SerializeField]
    private KinomiData kinomiData;    // 木の実データ

    public Text allKinomiNumText;
    public Text appleNumText;
    public Text orengeNumText;
    public Text bananaNumText;
    
    public int nowKinomiNum = 0;    // 今持っている木の実の合計数
    // それぞれの木の実の個数
    public int appleNum = 0;
    public int orengeNum = 0;
    public int bananaNum = 0;

    public static KinomiManager instance;

    private void Awake()
    {
        LoadKinomiSourceData();
        instance = this;
    }

    private void Update()
    {
        allKinomiNumText.text = nowKinomiNum.ToString();
        appleNumText.text = appleNum.ToString();
        orengeNumText.text = orengeNum.ToString();
        bananaNumText.text = bananaNum.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            LostAllKinomi();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            LostKinomi("リンゴ", 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LostKinomi("オレンジ", 1);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LostKinomi("バナナ", 1);
        }
    }

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
    public void CountItem(string kinomiName, int count)
    {
        // List内を検索
        foreach (KinomiData data in playerKinomiDataList)
        {
            if (data.name == kinomiName)
            {
                data.CountUp(count);
                break;
            }
        }

        // IDが一致しなければ、木の実を追加
        kinomiData = new KinomiData(kinomiName, count);
        playerKinomiDataList.Add(kinomiData);

        if (kinomiName == "リンゴ")
        {
            appleNum++;
        }
        else if (kinomiName == "オレンジ")
        {
            orengeNum++;
        }
        else if (kinomiName == "バナナ")
        {
            bananaNum++;
        }

        Debug.Log(kinomiData.name + "を取得");
        Debug.Log("持っている木の実の数：" + playerKinomiDataList.Count);
        nowKinomiNum++;
    }

    /// <summary>
    /// 木の実をロスト
    /// </summary>
    /// <param name="kinomiID">木の実のID</param>
    /// <param name="count">ロストする個数</param>
    public void LostKinomi(string kinomiName, int count)
    {
        // List内を検索
        foreach (KinomiData data in playerKinomiDataList)
        {
            if(data.name == kinomiName)
            {
                data.CountDown(count);

                playerKinomiDataList.Remove(data);

                if (kinomiName == "リンゴ")
                {
                    appleNum -= count;
                }
                else if (kinomiName == "オレンジ")
                {
                    orengeNum -= count;
                }
                else if (kinomiName == "バナナ")
                {
                    bananaNum -= count;
                }
                nowKinomiNum -= count;
                Debug.Log(data.name + "を " + count + "個ロスト");
                Debug.Log("持っている木の実の数：" + playerKinomiDataList.Count);
                break;
            }
            else
            {
                Debug.Log(kinomiName + "を所持していません");
            }
        }
    }

    /// <summary>
    /// すべての木の実をロスト
    /// </summary>
    public void LostAllKinomi()
    {
        // List内を検索
        for (int i = 0; i < playerKinomiDataList.Count; i++)
        {
            playerKinomiDataList.RemoveRange(0, nowKinomiNum);
        }
        nowKinomiNum = 0;
        appleNum = 0;
        orengeNum = 0;
        bananaNum = 0;

        Debug.Log("すべての木の実をロストしました");
        Debug.Log("持っている木の実の数：" + playerKinomiDataList.Count);
    }
}
