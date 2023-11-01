using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class TitleOption : MonoBehaviour
{
    [SerializeField] ActivateButtonCopy activateButton;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource moveSound;
    [SerializeField] List<Image> images;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;
    [SerializeField] int maxPageNum = 2;

    enum PageType
    {
        Front = 0,
        Middle,
        Back
    }

    PageType currentPageType = PageType.Front;
    int currentPageNum = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            clickSound.Play();
            activateButton.ActivateOrNotActivate(true);
            this.gameObject.SetActive(false);
        }

        bool isChangePage = false;

        if (currentPageType != PageType.Back && Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveSound.Play();
            isChangePage = true;
            currentPageNum++;
            if (currentPageNum >= maxPageNum)
            {
                currentPageType = PageType.Back;
            }
            else
            {
                currentPageType = PageType.Middle;
            }
        }
        if (currentPageType != PageType.Front && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveSound.Play();
            isChangePage = true;
            currentPageNum--;
            if (currentPageNum <= 1)
            {
                currentPageType = PageType.Front;
            }
            else
            {
                currentPageType = PageType.Middle;
            }
        }

        if (isChangePage)
        {
            int count = 1;
            foreach (var item in images)
            {
                if (count++ == currentPageNum)
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }
            switch (currentPageType)
            {
                case PageType.Front:
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(true);
                    break;
                case PageType.Middle:
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(true);
                    break;
                case PageType.Back:
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
