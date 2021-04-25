using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteOrder : MonoBehaviour
{
    
    public GameObject Player;
    public SpriteRenderer sprite;
    public bool spriteRendererNeedsDefining = true;

    void Awake()
    {
        Player = GameObject.Find("Player");
        if(spriteRendererNeedsDefining)
        {
            sprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        }
        
    }
    
    void Update()
    {
        if(Player.transform.position.y > transform.position.y)
        {
            sprite.sortingOrder = 11;
        } else
        {
            sprite.sortingOrder = 9;
        }
    }
}
