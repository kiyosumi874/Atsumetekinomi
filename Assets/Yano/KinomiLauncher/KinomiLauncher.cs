using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
/// <summary>
/// �؂̎����˃N���X
/// </summary>
public class KinomiLauncher : MonoBehaviour
{
     [SerializeField] private int needKinomiCount = 0;
    
    private void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void FireKinomi(Object kinomi)
    {
        kinomi.AddComponent<KinomiMover>();
    }
}