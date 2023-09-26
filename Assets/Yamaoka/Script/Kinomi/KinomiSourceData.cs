using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kinomi/KinomiData")]
public class KinomiSourceData : ScriptableObject
{
    /// <summary>
    /// 木の実の生成場所のデータ
    /// </summary>
    public enum GenerationLocation
    {
        Near,    // 巣の近く
        Far,     // 巣から遠い
        Middle   // 中間
    }

    [SerializeField]
    private int id;  // 木の実識別用ID
    [SerializeField]
    private string name;  // 木の実の名前
    [SerializeField]
    private GenerationLocation location;  // 木の実の生成場所

    /// <summary>
    /// 木の実のIDを取得
    /// </summary>
    public int kinomiID
    {
        get { return id; }
    }
    /// <summary>
    /// 木の実の名前を取得
    /// </summary>
    public string kinomiName
    {
        get { return name; }
    }
    /// <summary>
    /// 木の実の生成場所
    /// </summary>
    public GenerationLocation kinomiGeneratLocation
    {
        get { return location; }
    }
}
