using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class product : MonoBehaviour
{
    public GameObject Player;

    public GameObject popUpInfo;
    public SpriteRenderer sprite;
    public Transform spriteGameO;
    public Transform Hand;
    public bool holding;

    public string itemName;

    public BoxCollider2D coll;

    
    void Awake()
    {
        spriteGameO = transform.Find("Sprite");
        sprite = spriteGameO.gameObject.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
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

        if (holding)
        {
            transform.position = Hand.position;
            coll.enabled = false;
        } else
        {
            coll.enabled = true;
        }
    }
}
