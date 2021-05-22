using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stand : MonoBehaviour
{
    [Header("Gameobject Specific")]
    public int dir;
    [Header("Data")]
    public StandData originalStandData;
    public StandSceneData standData;
    public GameObject itemPrefab;
    [Header("Componets")]
    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D standCollider;
    public BoxCollider2D standPhysCollider;
    [Header("GUI")]
    public TextMeshProUGUI nameGUI;
    public TextMeshProUGUI itemNameGUI;
    public TextMeshProUGUI stockAmmountGUI;
    public Slider restockingProgressGUI;
    public TextMeshProUGUI restockingRemainingTimeGUI;
    [Header("Script")]
    public int currentStock;
    public float currentRestockTime;
    [Header("Debug")]
    public bool isRestocking;
    public int items;

    void FixedUpdate()
    {
        stockAmmountGUI.text = currentStock.ToString();

        if (isRestocking)
        {
            currentRestockTime -= Time.fixedDeltaTime;
            restockingProgressGUI.value = currentRestockTime;
            restockingRemainingTimeGUI.text = Mathf.Floor(currentRestockTime).ToString();
            if (currentRestockTime < 0)
            {
                currentStock = standData.maxStock;
                isRestocking = false;
                restockingProgressGUI.value = 0;
                restockingRemainingTimeGUI.text = "0";
            }
        }
    }

    public void Interact(ItemManager im)
    {
        Debug.Log("Interacting from " + im.gameObject.name + "!");

        if (currentStock < 1)
        { Restock(); return; }

        if (items >= standData.maxItemsInPlay)
            return;

        if (im.holding)
        { Debug.LogWarning("Player was holding something?"); return; }

        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        item.transform.Find("ItemAim").GetComponent<product>().itemData = standData.itemData;
        item.transform.Find("ItemAim").GetComponent<product>().InitializeData();

        product p = item.transform.Find("ItemAim").GetComponent<product>();

        im.PickupItem(p);

        currentStock -= 1;

        p.host = this;

        items++;
    }

    void Restock()
    {
        if (isRestocking)
        {Debug.Log("Already restocking!"); return;} 
        else {Debug.Log(gameObject.name + " has started restocking!");}

        currentRestockTime = Random.Range(standData.minRestockTime, standData.maxRestockTime);
        isRestocking = true;
    }

    #region Starting
    void Awake()
    {
        Innit();
    }

    void Innit()
    {
        standData = GetStandData();
        
        nameGUI.text = standData.standName;
        itemNameGUI.text = standData.itemData.itemName;
        currentStock = standData.maxStock;
        restockingProgressGUI.maxValue = standData.maxStock;

        standCollider.size = standData.rotData[dir].aimColl;
        standPhysCollider.size = standData.rotData[dir].physColl;
        SpriteRenderer.sprite = standData.rotData[dir].sprite;
    }

    StandSceneData GetStandData()
    {
        StandSceneData data = null;
        string currentSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < originalStandData.StandDatas.Length; i++)
        {
            if (originalStandData.StandDatas[i].sceneName == currentSceneName)
            {
                data = originalStandData.StandDatas[i];
            }
        }

        if (currentSceneName == "tl1")
        {
            data = originalStandData.StandDatas[0];
        }

        if (data == null)
            Debug.LogError("Stand data doesn't have an entry for this scene!");

        return data;
    }
    #endregion
}
