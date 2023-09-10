using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 木の実が巣に移動するのに必要なクラス
/// </summary>
public class KinomiMover : MonoBehaviour
{
    //到着する場所
    private Vector3 dropPoint= new Vector3(0,0,0);
    private Vector3 firstPos= new Vector3(0,0,0);
    //移動するのに必要な時間
    private float moveTime = 10.0f;
    //移動した時間
    private float movedTime = 0;
    //移動終了フラグ
    private bool isEndMove = false;

    private void Start()
    {
        firstPos= transform.position;
    }
    private void FixedUpdate()
    {
        if (!isEndMove)
        {
            //移動割合を出す
            float lerpValue = movedTime / moveTime;
            if (lerpValue < 1)
            {
                //移動する
                transform.position = Vector3.Lerp(firstPos, dropPoint, lerpValue);
            }
            //移動時間を経過させる
            movedTime += Time.deltaTime;
            //移動終了
            if (movedTime > moveTime)
            {
                transform.position = dropPoint;
                isEndMove = true;
            }
        }
    }
}
