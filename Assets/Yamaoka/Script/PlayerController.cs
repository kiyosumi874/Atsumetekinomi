using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 15.0f;
    public float dashSpeed = 200.0f;
    private float tempMoveSpeed;
    private Vector3 inputAxis;
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度

    private Vector3 moveDirection;   // 進行方向

    public GameObject footPrintPrefab;  // 足跡
    public Transform frontPos;
    public Transform backPos;

    public float time = 0;
    public float dashTime = 0;

    public double counter = 0;
    public double maxCounter = 5;

    public bool onIceFloor = false;     // 氷の床の上にいるかどうか
    public bool isDash = false;

    public ParticleSystem dashParticle; // ダッシュ時のエフェクト

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        tempMoveSpeed = moveSpeed;
        dashParticle.Stop();
    }

    private void Update()
    {
        // 木の実を捨てて、ダッシュする処理
        if(!isDash && KinomiManager.instance.nowKinomiNum > 0)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                KinomiManager.instance.LostKinomi();
                moveSpeed = dashSpeed;
                isDash = true;
            }
        }

        if(isDash)
        {
            dashTime += Time.deltaTime;
            dashParticle.Play();
        }

        if(dashTime > 1.0f)
        {
            dashTime = 0.0f;
            moveSpeed = tempMoveSpeed;
            isDash = false;
            dashParticle.Stop();
        }

        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.z = Input.GetAxis("Vertical");

        if(inputAxis != Vector3.zero)
        {
            time += Time.deltaTime;
            counter = 0;
        }
        
        if(time > 0.35f)
        {
            time = 0.0f;
            Instantiate(footPrintPrefab, frontPos.position, transform.rotation);
            Instantiate(footPrintPrefab, backPos.position, transform.rotation);
        }

        // GameStateに応じて、プレイヤーの行動を制限する
        if (GameManager.instance.gameState != GameState.InGame)
        {
            inputAxis = Vector3.zero;
        }

        // 入力がないときに、操作パネルを表示
        if (inputAxis == Vector3.zero)
        {
            counter += Time.deltaTime;
            if(counter >= maxCounter) 
            {
                counter = maxCounter;
                UIManager.instance.operationPanel.SetActive(true);
            }
        }
        else if (inputAxis != Vector3.zero
            && UIManager.instance.operationPanel.activeSelf)
        {
            DOVirtual.DelayedCall(1.0f,
           () => { UIManager.instance.operationPanel.SetActive(false); });
        }
    }

    private void FixedUpdate()
    {
        inputAxis.Normalize();
        rb.AddForce(-inputAxis * moveSpeed * moveForceMultiplier);
        // 速度の上限を設定
        float maxSpeedX = 10.0f;
        float maxSpeedZ = 10.0f;
        
        if(onIceFloor)
        {
            // 氷の床の上にいるときの速度
            maxSpeedX = 25.0f;
            maxSpeedZ = 25.0f;
        }
        else
        {
            maxSpeedX = 10.0f;
            maxSpeedZ = 10.0f;
        }

        if(isDash)
        {
            maxSpeedX = 30.0f;
            maxSpeedZ = 30.0f;
        }

        // 速度上限を超えた際の処理
        if(rb.velocity.x >= maxSpeedX)
        {
            rb.velocity = new Vector3(maxSpeedX, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z >= maxSpeedZ)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeedZ);
        }

        if (rb.velocity.x <= -maxSpeedX)
        {
            rb.velocity = new Vector3(-maxSpeedX, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z <= -maxSpeedZ)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxSpeedZ);
        }

        // 進行方向に回転させる
        moveDirection = -inputAxis;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10.0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "IceFloor")
        {
            onIceFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "IceFloor")
        {
            onIceFloor = false;
        }
    }

}
