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
        Middle,  // 中間
        Far,     // 巣から遠い
    }

    public string kinomiName;  // 木の実の名前
    public GenerationLocation generatLocation;  // 木の実の生成場所

    public int score = 50;

    public static Kinomi instance;
    public ParticleSystem effect;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // 木の実をY軸回転させる
        this.gameObject.transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }

    public int GetKinomiScore()
    {
        return score;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //effect.Play();
            EffectManager.instance.PlayEffect(this.transform, effect.startColor);
            KinomiManager.instance.CountItem(kinomiName, 1);
            KinomiGenerator.instance.nowKinomiNum--;
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Kinomi")
        {
            //effect.Play();
            EffectManager.instance.PlayEffect(this.transform, effect.startColor);
            KinomiGenerator.instance.nowKinomiNum--;
            Destroy(this.gameObject);
        }
    }
}
