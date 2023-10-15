using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 15.0f;
    private float tempMoveSpeed;
    private Vector3 inputAxis;
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度

    private Vector3 moveDirection;   // 進行方向

    public GameObject footPrintPrefab;  // 足跡
    public Transform frontPos;
    public Transform backPos;

    public float time = 0;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        tempMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.z = Input.GetAxis("Vertical");

        if(inputAxis != Vector3.zero)
        {
            time += Time.deltaTime;
        }
        
        if(time > 0.35f)
        {
            time = 0.0f;
            Instantiate(footPrintPrefab, frontPos.position, transform.rotation);
            Instantiate(footPrintPrefab, backPos.position, transform.rotation);
        }

        // GameStateに応じて、プレイヤーの行動を制限する
        if(GameManager.instance.gameState != GameState.InGame)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = tempMoveSpeed;
        }

        // 入力がないときに、操作パネルを表示
        if(inputAxis == Vector3.zero)
        {
            DOVirtual.DelayedCall(1.0f,
            () => { UIManager.instance.operationPanel.SetActive(true); });
            //UIManager.instance.operationPanel.SetActive(true);
        }
        else
        {
            UIManager.instance.operationPanel.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        inputAxis.Normalize();
        rb.AddForce(inputAxis * -moveSpeed * moveForceMultiplier);

        // 進行方向に回転させる
        moveDirection = -inputAxis;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10.0f);
    }
}
