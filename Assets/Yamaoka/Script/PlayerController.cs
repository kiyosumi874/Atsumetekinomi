using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 15.0f;
    private Vector3 inputAxis;
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度

    private Vector3 moveDirection;   // 進行方向

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.z = Input.GetAxis("Vertical");
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
