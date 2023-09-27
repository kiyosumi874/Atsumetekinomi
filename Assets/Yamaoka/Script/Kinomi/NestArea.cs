using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巣に戻ってきたときの処理
/// </summary>
public class NestArea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // エリアにプレイヤーが入ってきたら
        if(other.gameObject.tag == "Player")
        {
            ComboManager.instance.UseCombo();
            ComboManager.instance.GetNotComboScore();
            KinomiManager.instance.LostAllKinomi();
        }
    }
}
