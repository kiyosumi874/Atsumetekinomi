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

        // �i�s�����ɉ�]������
        moveDirection = -inputAxis;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 10.0f);
    }
}
