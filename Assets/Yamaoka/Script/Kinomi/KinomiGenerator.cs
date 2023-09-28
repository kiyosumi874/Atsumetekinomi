using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �؂̎������N���X
/// </summary>
public class KinomiGenerator : MonoBehaviour
{
    // ��������؂̎��𐶐��ꏊ���Ƃɂ܂Ƃ߂�List
    [SerializeField]
    private List<GameObject> nearKinomis = new List<GameObject>();      // �ߏ�
    [SerializeField]
    private List<GameObject> middleKinomis = new List<GameObject>();    // ����
    [SerializeField]
    private List<GameObject> farKinomis = new List<GameObject>();       // ����

    // �؂̎��̐����͈�
    // �ߏ�
    [SerializeField]
    Transform NrangeA;
    [SerializeField]
    Transform NrangeB;
    // ����
    [SerializeField]
    Transform MrangeA;
    [SerializeField]
    Transform MrangeB;
    // ����
    [SerializeField]
    Transform FrangeA;
    [SerializeField]
    Transform FrangeB;

    public float nowKinomiNum = 0;      // ���݂̑��؂̎���
    public float maxKinomiNum = 20;     // �؂̎��̍ő吶����

    // ���ꂼ��̖؂̎��̐�������
    public float createNearTime;
    public float createMiddleTime;
    public float createFarTime;

    public static KinomiGenerator instance;  // �C���X�^���X

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // �ő吔�𒴂�����A�������Ȃ�
        if(nowKinomiNum >= maxKinomiNum)
        {
            return;
        }

        createNearTime = createNearTime + Time.deltaTime;
        createMiddleTime = createMiddleTime + Time.deltaTime;
        createFarTime = createFarTime + Time.deltaTime;

        // �ߏ�̖؂̎�����
        if (createNearTime > 5.0f)
        {
            CreateKinomi(Kinomi.GenerationLocation.Near);
            createNearTime = 0.0f;
        }
        // ���Ԃ̖؂̎�����
        if (createMiddleTime > 8.0f)
        {
            CreateKinomi(Kinomi.GenerationLocation.Middle);
            createMiddleTime = 0.0f;
        }
        // �����̖؂̎�����
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
