using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    public int id;
    public Sprite sprite;
    public SpriteRenderer image;

    void Awake()
    {
        image.sprite = sprite;
    }
}
