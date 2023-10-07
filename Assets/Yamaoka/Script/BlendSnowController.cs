using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendSnowController : MonoBehaviour
{
    public float speed = 0.000001f;    // 雪が積もる速さ
    private float amount;        // 積雪量
    public float max = 1.0f;     // 最大積雪量
    public Material mat;         // マテリアルを取得

    // Start is called before the first frame update
    void Start()
    {
        // 最初からある程度積もった状態から始める
        amount = 0.004f;
    }

    private void FixedUpdate()
    {
        if(amount < max)
        {
            // 積雪量の計算
            amount = amount + speed;
            // シェーダー定数設定
            mat.SetFloat("_Amount", amount);
        }
    }
}
