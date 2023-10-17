using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    //変数宣言

    //移動関連
    private Rigidbody rigidbody;                 //リジッドボディ
    private Vector3 direction_vector;            //移動方向のベクトル
    public const float MOVESPEED = 5.0f;         //移動速度

    //アニメーション関連
    private Animator animator;                  //アニメーター
    private bool isMove;                        //今動いているかどうか

    //角度関連
    Vector3 prev_position;                      //前フレームの座標
    public const float ROTATE_SPEED = 10.0f;    //回転する速さ
    

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
        //移動量を計算する
        Vector3 current_potision = transform.position;
        Vector3 delta_movement = current_potision - prev_position;
        prev_position = current_potision;
        //移動中なら少しずつ回転させる
        if (isMove)
        {
            transform.forward = Vector3.Slerp(transform.forward, delta_movement, Time.deltaTime * ROTATE_SPEED);
        }
    }
}
