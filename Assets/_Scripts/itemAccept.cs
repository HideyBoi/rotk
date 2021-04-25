﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class itemAccept : MonoBehaviour
{
    public GameObject Karen;
    public GameObject itemPrefab;

    public ratingManager rm;
    public GameObject wrongEffectPrefab;
    public GameObject correctEffectPrefab;

    public Item[] items;

    public Item selectedItem;

    public TextMeshProUGUI timeTex;
    public float time;
    public TextMeshProUGUI itemName;
    public string ItemName;

    void Awake()
    {
        rm = GameObject.Find("Rating Manager").GetComponent<ratingManager>();

        int rng = Random.Range(0, items.Length);

        selectedItem = items[rng];

        itemName.text = selectedItem.itemName;
        ItemName = selectedItem.itemName;

        for (int i = 0; i < selectedItem.scene.Length; i++)
        {
            if (selectedItem.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                time = selectedItem.scene[i].time;
            }
        }
    }

    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;

        timeTex.text = Mathf.FloorToInt(time).ToString();

        if (time < 0)
        {
            Debug.Log("Out of time!");
            Wrong();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (ItemName == other.GetComponent<product>().itemData.itemName)
            {
                Correct();
                Debug.Log("Correct Item!");
                Destroy(other.gameObject);
            } else
            {
                Wrong();
                Debug.Log("Wrong Item!");
                Destroy(other.gameObject);
            }
        }
    }

    void Wrong()
    {
        Instantiate(wrongEffectPrefab, transform.position, Quaternion.identity);
        rm.rating -= 2;
        RespawnItem();
        Destroy(Karen);
    }

    void Correct()
    {
        Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        rm.rating += 1;
        RespawnItem();
        Destroy(Karen);
    }

    void RespawnItem()
    {
        Vector3 itemPos = Vector3.zero;

        for (int i = 0; i < selectedItem.scene.Length; i++)
        {
            if (selectedItem.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                itemPos = selectedItem.scene[i].spawnPos;
            }
        }

        GameObject item = Instantiate(itemPrefab, itemPos, Quaternion.identity);
        item.GetComponent<product>().itemData = selectedItem;
        item.GetComponent<product>().InitializeData();
    }
}