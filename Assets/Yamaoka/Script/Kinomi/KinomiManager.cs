using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// 木の実データ
/// </summary>
[System.Serializable]
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

    [SerializeField]
    private List<Kinomi> kinomis = new List<Kinomi>();

    public Text allKinomiNumText;
    public Text appleNumText;
    public Text orengeNumText;
    public Text bananaNumText;
    public Text lemonNumText;
    public Text watermelonNumText;

    public int nowKinomiNum = 0;    // 今持っている木の実の合計数
    public int maxKinomiNum = 10;   // 所持できる木の実の最大数
    // それぞれの木の実の個数
    public int appleNum = 0;
    public int orengeNum = 0;
    public int bananaNum = 0;
    public int lemonNum = 0;
    public int watermelonNum = 0;
    // 木の実所持判定フラグ
    public bool hasApple = false;
    public bool hasOrenge = false;
    public bool hasBanana = false;
    public bool hasLemon = false;
    public bool hasWatermelon = false;

    public int appleScore = 0;
    public int orengeScore = 0;
    public int bananaScore = 0;
    public int lemonScore = 0;
    public int watermelonScore = 0;

    public static KinomiManager instance;   // インスタンス

    private void Awake()
    {
        LoadKinomiSourceData();
        instance = this;
        SetKinomisScore();
    }

    private void Update()
    {
        nowKinomiNum = appleNum + orengeNum + bananaNum + lemonNum + watermelonNum;
        CheckHasKinomi();
        SetMinAllKinomisNum();
        allKinomiNumText.text = nowKinomiNum.ToString();
        appleNumText.text = "x" + appleNum.ToString();
        orengeNumText.text = "x" + orengeNum.ToString();
        bananaNumText.text = "x" + bananaNum.ToString();
        lemonNumText.text = "x" + lemonNum.ToString();
        watermelonNumText.text = "x" + watermelonNum.ToString();

        UIManager.instance.ChangeKinomiImgAlpha(hasApple, hasLemon, hasOrenge, hasBanana, hasWatermelon);
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
    /// 各木の実のスコアを設定
    /// </summary>
    public void SetKinomisScore()
    {
        for (int i = 0; i < kinomis.Count; i++)
        {
            switch (kinomis[i].kinomiName)
            {
                case "リンゴ":
                    appleScore = kinomis[i].GetKinomiScore();
                    break;
                case "オレンジ":
                    orengeScore = kinomis[i].GetKinomiScore();
                    break;
                case "バナナ":
                    bananaScore = kinomis[i].GetKinomiScore();
                    break;
                case "レモン":
                    lemonScore = kinomis[i].GetKinomiScore();
                    break;
                case "スイカ":
                    watermelonScore = kinomis[i].GetKinomiScore();
                    break;
            }
        }
    }

    /// <summary>
    /// 木の実を持っているかどうか検証
    /// </summary>
    public void CheckHasKinomi()
    {
        if(appleNum <= 0)
        {
            hasApple = false;
        }
        else
        {
            hasApple = true;
        }
        if(orengeNum <= 0)
        {
            hasOrenge = false;
        }
        else
        {
            hasOrenge = true;
        }
        if(bananaNum <= 0)
        {
            hasBanana = false;
        }
        else
        {
            hasBanana = true;
        }
        if (lemonNum <= 0)
        {
            hasLemon = false;
        }
        else
        {
            hasLemon = true;
        }
        if (watermelonNum <= 0)
        {
            hasWatermelon = false;
        }
        else
        {
            hasWatermelon = true;
        }
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

        if(nowKinomiNum < maxKinomiNum)
        {
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
            else if (kinomiName == "レモン")
            {
                lemonNum++;
            }
            else if (kinomiName == "スイカ")
            {
                watermelonNum++;
            }
        }

        if(nowKinomiNum >= maxKinomiNum)
        {
            nowKinomiNum = maxKinomiNum;
        }
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
                else if (kinomiName == "レモン")
                {
                    lemonNum -= count;
                }
                else if (kinomiName == "スイカ")
                {
                    watermelonNum -= count;
                }
                //nowKinomiNum -= count;
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
        // 所持している木の実のデータをクリア
        playerKinomiDataList.Clear();

        nowKinomiNum = 0;
        appleNum = 0;
        orengeNum = 0;
        bananaNum = 0;
        lemonNum = 0;
        watermelonNum = 0;
        Debug.Log("すべての木の実をロストしました");
        Debug.Log("持っている木の実の数：" + playerKinomiDataList.Count);
    }

    /// <summary>
    /// すべての木の実の所持数の最小値を設定
    /// </summary>
    public void SetMinAllKinomisNum()
    {
        if(appleNum <= 0)
        {
            appleNum = 0;
        }
        if (orengeNum <= 0)
        {
            orengeNum = 0;
        }
        if (bananaNum <= 0)
        {
            bananaNum = 0;
        }
        if (lemonNum <= 0)
        {
            lemonNum = 0;
        }
        if (watermelonNum <= 0)
        {
            watermelonNum = 0;
        }
    }
}
