using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木の実生成クラス
/// </summary>
public class KinomiGenerator : MonoBehaviour
{
    // 生成する木の実を生成場所ごとにまとめたList
    [SerializeField]
    private List<GameObject> nearKinomis = new List<GameObject>();      // 近場
    [SerializeField]
    private List<GameObject> middleKinomis = new List<GameObject>();    // 中間
    [SerializeField]
    private List<GameObject> farKinomis = new List<GameObject>();       // 遠方

    // 木の実の生成範囲
    // 近場
    [SerializeField]
    Transform NrangeA;
    [SerializeField]
    Transform NrangeB;
    // 中間
    [SerializeField]
    Transform MrangeA;
    [SerializeField]
    Transform MrangeB;
    // 遠方
    [SerializeField]
    Transform FrangeA;
    [SerializeField]
    Transform FrangeB;

    public float nowKinomiNum = 0;      // 現在の総木の実数
    public float maxKinomiNum = 20;     // 木の実の最大生成数

    // それぞれの木の実の生成時間
    public float createNearTime;
    public float createMiddleTime;
    public float createFarTime;

    public static KinomiGenerator instance;  // インスタンス

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // 最大数を超えたら、何もしない
        if(nowKinomiNum >= maxKinomiNum)
        {
            return;
        }

        createNearTime = createNearTime + Time.deltaTime;
        createMiddleTime = createMiddleTime + Time.deltaTime;
        createFarTime = createFarTime + Time.deltaTime;

        // 近場の木の実生成
        if (createNearTime > 5.0f)
        {
            CreateKinomi(Kinomi.GenerationLocation.Near);
            createNearTime = 0.0f;
        }
        // 中間の木の実生成
        if (createMiddleTime > 8.0f)
        {
            CreateKinomi(Kinomi.GenerationLocation.Middle);
            createMiddleTime = 0.0f;
        }
        // 遠方の木の実生成
        if (createFarTime > 10.0f)
        {
            CreateKinomi(Kinomi.GenerationLocation.Far);
            createFarTime = 0.0f;
        }
    }

    public void CreateKinomi(Kinomi.GenerationLocation generatLocation)
    {
        switch (generatLocation)
        {
            case Kinomi.GenerationLocation.Near:
                float nx = Random.Range(NrangeA.position.x, NrangeB.position.x);
                float nz = Random.Range(NrangeA.position.z, NrangeB.position.z);
                int Nrand = Random.RandomRange(0, nearKinomis.Count);

                Instantiate(nearKinomis[Nrand], new Vector3(nx, 2, nz), nearKinomis[Nrand].transform.rotation);
                //Debug.Log("CreateNear");
                break;
            case Kinomi.GenerationLocation.Middle:
                float mx = Random.Range(MrangeA.position.x, MrangeB.position.x);
                float mz = Random.Range(MrangeA.position.z, MrangeB.position.z);
                int Mrand = Random.RandomRange(0, middleKinomis.Count);

                Instantiate(middleKinomis[Mrand], new Vector3(mx, 2, mz), middleKinomis[Mrand].transform.rotation);
                //Debug.Log("CreateMiddle");
                break;
            case Kinomi.GenerationLocation.Far:
                float fx = Random.Range(FrangeA.position.x, FrangeB.position.x);
                float fz = Random.Range(FrangeA.position.z, FrangeB.position.z);
                int Frand = Random.RandomRange(0, farKinomis.Count);

                Instantiate(farKinomis[Frand], new Vector3(fx, 2, fz), farKinomis[Frand].transform.rotation);
                //Debug.Log("CreateFar");
                break;
        }
        nowKinomiNum++;
    }
}
