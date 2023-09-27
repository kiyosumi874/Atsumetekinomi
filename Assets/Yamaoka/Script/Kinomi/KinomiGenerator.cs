using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ñÿÇÃé¿ê∂ê¨ÉNÉâÉX
/// </summary>
public class KinomiGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject apple;
    [SerializeField]
    private GameObject orenge;
    [SerializeField]
    private GameObject banana;

    [SerializeField]
    private List<GameObject> kinomis = new List<GameObject>();


    [SerializeField]
    Transform NrangeA;
    [SerializeField]
    Transform NrangeB;

    public bool createNear;
    public bool createMiddle;
    public bool createFar;

    public float time;

    Kinomi.GenerationLocation generationLocation;


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

        for(int i = 0; i < kinomis.Count; i++)
        {
            generationLocation = kinomis[i].GetComponent<Kinomi>().generatLocation;
            switch(generationLocation)
            {
                case Kinomi.GenerationLocation.Near:

                    break;
            }
        }


        if(time > 5.0f)
        {
            for (int i = 0; i < kinomis.Count; i++)
            {
                generationLocation = kinomis[i].GetComponent<Kinomi>().generatLocation;
                switch (generationLocation)
                {
                    case Kinomi.GenerationLocation.Near:
                        float x = Random.Range(NrangeA.position.x, NrangeB.position.x);
                        float z = Random.Range(NrangeA.position.z, NrangeB.position.z);

                        Instantiate(kinomis[i], new Vector3(x, 2, z), kinomis[i].transform.rotation);
                        break;
                }
            }

            //if (apple.GetComponent<Kinomi>().generatLocation == Kinomi.GenerationLocation.Near)
            //{
            //    float x = Random.Range(NrangeA.position.x, NrangeB.position.x);
            //    float z = Random.Range(NrangeA.position.z, NrangeB.position.z);

            //    Instantiate(apple, new Vector3(x, 2, z), apple.transform.rotation);
            //}

            time = 0.0f;
        }
    }
}
