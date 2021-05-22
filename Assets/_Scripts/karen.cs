using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karen : MonoBehaviour
{
    public Sprite[] sprite;
    public SpriteRenderer image;

    public int spriteID;
    public int id;

    void Awake()
    { 
        int RNG = Random.Range(0, 4);
        spriteID = RNG;
        image.sprite = sprite[RNG];
    }
}
