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
    public List<Image> images = new List<Image>();
    //public List<Sprite> sprites = new List<Sprite>();
    public List<SpriteData> sprites = new List<SpriteData>();

    public static ChangeImage instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    //images[0].sprite = sprites[0].sprite;
        //    ChangeMyKinomiImage(10, "ƒXƒCƒJ");
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    //images[0].sprite = sprites[0].sprite;
        //    ResetMyKinomiImage();
        //}

    }

    public void ChangeMyKinomiImage(int changeCount, string getKinomiName)
    {
        Debug.Log(changeCount);
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

    public void ResetMyKinomiImage()
    {
        for(int i = 0; i < images.Count; i++)
        {
            images[i].sprite = null;
        }
        Debug.Log("Reset");
    }

    public void ResetLastKinomiImage(int lastNum)
    {
        images[lastNum].sprite = null;
    }
}
