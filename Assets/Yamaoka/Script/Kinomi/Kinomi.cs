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
    public GenerationLocation generatLocation;  // 木の実の生成場所

    public int score = 50;

    public static Kinomi instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            KinomiManager.instance.CountItem(kinomiName, 1);
            this.gameObject.SetActive(false);
        }
    }
}
