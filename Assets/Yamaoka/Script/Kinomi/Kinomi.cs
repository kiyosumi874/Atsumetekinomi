using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 木の実クラス
/// </summary>
public class Kinomi : MonoBehaviour
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
    private string kinomiName;  // 木の実の名前
    [SerializeField]
    GenerationLocation generatLocation;  // 木の実の生成場所


    [SerializeField]
    private KinomiManager kinomiManager;

    public void OnCollisionEnter(Collision collision)
    {
        kinomiManager.CountItem(kinomiName, 1);
        this.gameObject.SetActive(false);
    }
}
