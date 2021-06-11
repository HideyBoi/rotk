using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    public GameObject[] pageObjs;
    public GameObject[] buttonObjs;
    public Sprite[] sprites;

    public void SwitchTab(int page)
    {
        for (int i = 0; i < pageObjs.Length; i++)
        {
            pageObjs[i].SetActive(false);
            buttonObjs[i].GetComponent<Image>().sprite = sprites[0];
        }

        pageObjs[page].SetActive(true);
        buttonObjs[page].GetComponent<Image>().sprite = sprites[1];
    }
}
