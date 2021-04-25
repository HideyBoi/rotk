using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karen : MonoBehaviour
{
    public Sprite[] sprite;
    public SpriteRenderer image;

    void Awake()
    { 
        int RNG = Random.Range(0, 4);

        image.sprite = sprite[RNG];
    }
   
}
