using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class product : MonoBehaviour
{
    public Item itemData;

    [Header("Sprite Ordering")]
    public GameObject Player;
    public SpriteRenderer sprite;
    public Transform spriteGameO;
    public Transform Hand;
    public bool holding;

    [Header("Item Data")]
    public string itemName;

    public BoxCollider2D coll;

    
    void Awake()
    {
        if (itemData != null)
            InitializeData();

        spriteGameO = transform.Find("Sprite");
        sprite = spriteGameO.gameObject.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Player == null)
            return;
        
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

    public void InitializeData()
    {
        itemName = itemData.itemName;
        coll.size = itemData.collBounds;
        sprite.sprite = itemData.itemSprite;
    }
}
