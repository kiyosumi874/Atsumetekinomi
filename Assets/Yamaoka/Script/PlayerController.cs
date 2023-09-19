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

        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * moveSpeed;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * moveSpeed;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * moveSpeed;
        }

        // Aキー（左移動）
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
