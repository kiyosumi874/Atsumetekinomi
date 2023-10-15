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
    public float moveForceMultiplier;    // �ړ����x�̓��͂ɑ΂���Ǐ]�x

    private Vector3 moveDirection;   // �i�s����

    public GameObject footPrintPrefab;  // ����
    public Transform frontPos;
    public Transform backPos;

    public float time = 0;

    public double counter = 0;
    public double maxCounter = 5;

    public bool onIceFloor = false;     // �X�̏��̏�ɂ��邩�ǂ���

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
            counter = 0;
        }
        
        if(time > 0.35f)
        {
            time = 0.0f;
            Instantiate(footPrintPrefab, frontPos.position, transform.rotation);
            Instantiate(footPrintPrefab, backPos.position, transform.rotation);
        }

        // GameState�ɉ����āA�v���C���[�̍s���𐧌�����
        if(GameManager.instance.gameState != GameState.InGame)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = tempMoveSpeed;
        }

        // ���͂��Ȃ��Ƃ��ɁA����p�l����\��
        if(inputAxis == Vector3.zero)
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
        // ���x�̏����ݒ�
        float maxSpeedX = 10.0f;
        float maxSpeedZ = 10.0f;
        
        if(onIceFloor)
        {
            // �X�̏��̏�ɂ���Ƃ��̑��x
            maxSpeedX = 25.0f;
            maxSpeedZ = 25.0f;
        }
        else
        {

            maxSpeedX = 10.0f;
            maxSpeedZ = 10.0f;
        }
        // ���x����𒴂����ۂ̏���
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

        // �i�s�����ɉ�]������
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
