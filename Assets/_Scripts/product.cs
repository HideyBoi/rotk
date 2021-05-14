﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class product : MonoBehaviour
{
    public Item itemData;
    public int id;
    public Stand host;

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
        
        for (int i = 0; i < itemData.scene.Length; i++)
        {
            if (itemData.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                sprite.sprite = itemData.scene[i].itemSprite;
            }
        }

        itemNameText.text = itemData.itemName;
    }

    void OnDestroy()
    {
        if (host != null)   
            host.items--;
    }
}
