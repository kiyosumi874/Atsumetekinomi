using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 15.0f;
    private Vector3 inputAxis;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.z = Input.GetAxis("Vertical");

        // W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * moveSpeed;
        }

        // S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * moveSpeed;
        }

        // D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * moveSpeed;
        }

        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * moveSpeed;
        }
    }

    //private void FixedUpdate()
    //{
    //    rb.velocity = inputAxis.normalized * moveSpeed;
    //}
}
