using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ゲームステータスをBeforeGameに変更
    /// </summary>
    public void ChangeGameStatus()
    {
        GameManager.instance.ChangeGameState(GameState.BeforeGame);
    }
}
