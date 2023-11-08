using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector;
    void Update()
    {
        this.gameObject.transform.Rotate(rotationVector * Time.deltaTime);
    }
}
