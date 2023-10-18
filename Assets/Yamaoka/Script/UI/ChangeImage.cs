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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            images[0].sprite = sprites[0].sprite;
        }
    }

    public void ChangeMyKinomiImage(int changeCount, string getKinomiName)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            if(getKinomiName == sprites[i].spriteName)
            {
                images[changeCount].sprite = sprites[i].sprite;
            }
        }
    }
}
