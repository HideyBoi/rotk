using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Item[] avaliableItems;

    public SceneData currentStage;

    public GameObject itemPrefab;

    void Start()
    {
        avaliableItems = currentStage.items;
    }
}
