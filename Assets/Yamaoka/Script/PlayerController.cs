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
    public float moveForceMultiplier;    // �ړ����x�̓��͂ɑ΂���Ǐ]�x

    private Vector3 moveDirection;   // �i�s����

    public GameObject footPrintPrefab;  // ����
    public Transform frontPos;
    public Transform backPos;

    public float time = 0;
    public float dashTime = 0;

    public double counter = 0;
    public double maxCounter = 5;

    public bool onIceFloor = false;     // �X�̏��̏�ɂ��邩�ǂ���
    public bool isDash = false;

    public ParticleSystem dashParticle; // �_�b�V�����̃G�t�F�N�g

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        tempMoveSpeed = moveSpeed;
        dashParticle.Stop();
    }

    private void Update()
    {
        // �؂̎����̂ĂāA�_�b�V�����鏈��
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

        // GameState�ɉ����āA�v���C���[�̍s���𐧌�����
        if (GameManager.instance.gameState != GameState.InGame)
        {
            inputAxis = Vector3.zero;
        }

        // ���͂��Ȃ��Ƃ��ɁA����p�l����\��
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

        if(isDash)
        {
            maxSpeedX = 30.0f;
            maxSpeedZ = 30.0f;
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
