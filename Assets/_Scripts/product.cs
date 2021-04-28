using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public BoxCollider2D aimColl;

    [Header("Tooltips")]
    public TextMeshProUGUI itemNameText;

    
    void Awake()
    {
        if (itemData != null)
            InitializeData();

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
            transform.parent.position = Hand.position;
            coll.enabled = false;
            aimColl.enabled = false;
        } else
        {
            coll.enabled = true;
            aimColl.enabled = true;
        }
    }

    public void InitializeData()
    {
        itemName = itemData.itemName;
        coll.size = itemData.collBounds;
        aimColl.size = itemData.aimCollBounds;
        aimColl.offset = Vector2.up * itemData.aimCollBounds.y / 2;
        sprite.sprite = itemData.itemSprite;

        itemNameText.text = itemData.itemName;
    }
}
