using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StandSceneData
{
    [Header("Appearacnce")]
    public string standName;
    [Header("Magic Words")]
    public Item itemData;
    public string sceneName;
    public int maxItemsInPlay;
    public int maxStock;
    public float maxRestockTime;
    public float minRestockTime;
    public StandRotData[] rotData;
}
