using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���I���������s���N���X
/// </summary>
public class GameEnd : MonoBehaviour
{
    public void EndGame()
    {
        // �ۑ����Ă��郉���L���O�f�[�^���폜����
        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        //�Q�[���v���C�I��
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
