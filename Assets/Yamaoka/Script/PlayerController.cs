using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 15.0f;
    private Vector3 inputAxis;
    public float moveForceMultiplier;    // ˆÚ“®‘¬“x‚Ì“ü—Í‚É‘Î‚·‚é’Ç]“x

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
    }
}
