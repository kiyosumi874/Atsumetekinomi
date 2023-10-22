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
    public List<Image> images = new List<Image>();  // �����؂̎�UI
    //public List<Sprite> sprites = new List<Sprite>();
    public List<SpriteData> sprites = new List<SpriteData>();   // ���ꂼ��̖؂̎��̉摜
    public Sprite defaultSprite;    // �؂̎��������Ă��Ȃ��Ƃ��̉摜

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
    /// �����؂̎�UI��؂̎��̉摜�ɐ؂�ւ���
    /// </summary>
    /// <param name="changeCount">�؂�ւ���摜�̔ԍ�</param>
    /// <param name="getKinomiName">�擾�����؂̎��̖��O</param>
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
    /// �����؂̎�UI�����Z�b�g����
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
    /// �Ō�Ɏ擾�����؂̎��摜���f�t�H���g�ɐ؂�ւ���
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
