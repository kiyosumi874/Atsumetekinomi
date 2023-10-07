using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 球を円周上で動かす
/// 巣の有効エリアを表示するのに使用
/// </summary>
public class sphereMover : MonoBehaviour
{
    public float radius;    // 円の半径
    public Transform centerPosition;
    public float rotateSpeed = 2.0f;
    Vector3 initPos;    // 初期位置


    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(centerPosition.position.x + radius, 2f, centerPosition.position.z + radius);
        this.gameObject.transform.position = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.RotateAround(centerPosition.position, Vector3.up, rotateSpeed);
    }
}
