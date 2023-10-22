using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SpriteData
{
    public string spriteName;
    public Sprite sprite;
}

public class ChangeImage : MonoBehaviour
{
    public List<Image> images = new List<Image>();  // 所持木の実UI
    //public List<Sprite> sprites = new List<Sprite>();
    public List<SpriteData> sprites = new List<SpriteData>();   // それぞれの木の実の画像
    public Sprite defaultSprite;    // 木の実を持っていないときの画像

    public static ChangeImage instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for(int i  = 0; i < images.Count; i++) 
        {
            images[i].sprite = defaultSprite;
        }
    }

    /// <summary>
    /// 所持木の実UIを木の実の画像に切り替える
    /// </summary>
    /// <param name="changeCount">切り替える画像の番号</param>
    /// <param name="getKinomiName">取得した木の実の名前</param>
    public void ChangeMyKinomiImage(int changeCount, string getKinomiName)
    {
        //Debug.Log(changeCount);
        if(changeCount > 10)
        {
            return;
        }

        for(int i = 0; i < sprites.Count; i++)
        {
            if(getKinomiName == sprites[i].spriteName)
            {
                images[changeCount].sprite = sprites[i].sprite;
            }
        }
    }

    /// <summary>
    /// 所持木の実UIをリセットする
    /// </summary>
    public void ResetMyKinomiImage()
    {
        for(int i = 0; i < images.Count; i++)
        {
            images[i].sprite = defaultSprite;
        }
        Debug.Log("Reset");
    }

    /// <summary>
    /// 最後に取得した木の実画像をデフォルトに切り替える
    /// </summary>
    /// <param name="lastNum"></param>
    public void ResetLastKinomiImage(int lastNum)
    {
        Debug.Log(lastNum + "Lastttt");
        if (lastNum > 10)
        {
            return;
        }
        images[lastNum].sprite = defaultSprite;
    }
}
