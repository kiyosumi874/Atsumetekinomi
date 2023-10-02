using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    //変数宣言

    //移動関連
    private Rigidbody rigidbody;            //リジッドボディ
    private Vector3 direction_vector;       //移動方向のベクトル
    public const float MOVESPEED = 5.0f;   //移動速度

    //アニメーション関連
    private Animator animator;  //アニメーター
    private bool isMove;        //今動いているかどうか

    //角度関連
    float round = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動関連初期化
        Vector3 vector = new Vector3();
        isMove = false;

        //もし→キーを押されたら移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //ベクトルに移動方向を代入する
            vector.x = -1;
            isMove = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //ベクトルに移動方向を代入する
            vector.x = 1;
            isMove = true;
        }
        //z軸の移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //ベクトルに移動方向を代入する
            vector.z = 1;
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //ベクトルに移動方向を代入する
            vector.z = -1;
            isMove = true;
        }

        //正規化して移動量をかける
        vector.Normalize();
        direction_vector = vector * MOVESPEED;
        rigidbody.velocity = direction_vector;

        //アニメーション制御
        if(isMove)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        //角度制御
        //TODO：左方向に入力すると回転が上手くいかない。修正すること
        //Transform transform = this.transform;
        //Vector3 local_angle = transform.localEulerAngles;
        ////動いているなら回転分を計算
        //if(isMove)
        //{
        //    round = Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
        //}
        
        //Debug.Log(round);
        //if(local_angle.y > round)
        //{
        //    local_angle.y -= 1.0f;
        //}
        //else if(local_angle.y < round)
        //{
        //    local_angle.y += 1.0f;
        //}
        //transform.eulerAngles = local_angle;
    }
}
