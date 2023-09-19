using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //変数宣言
    private Rigidbody rigidbody;    //リジッドボディ
    Vector3 direction_vector;   //移動方向のベクトル
    public const float MOVESPEED = 5.0f;   //移動速度
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //もし→キーを押されたら移動
        Vector3 vector = new Vector3();
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //ベクトルに移動方向を代入する
            vector.x = 1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //ベクトルに移動方向を代入する
            vector.x = -1;
        }
        //z軸の移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //ベクトルに移動方向を代入する
            vector.z = -1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //ベクトルに移動方向を代入する
            vector.z = 1;
        }

        //正規化して移動量をかける
        vector.Normalize();
        direction_vector = vector * MOVESPEED;
        rigidbody.velocity = direction_vector;
    }
}
