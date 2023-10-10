using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム終了処理を行うクラス
/// </summary>
public class GameEnd : MonoBehaviour
{
    public void EndGame()
    {
        // 保存しているランキングデータを削除する
        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        //ゲームプレイ終了
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
