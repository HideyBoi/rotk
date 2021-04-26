using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Item[] avaliableItems;

    public GameObject itemPrefab;

    void Start()
    {
        for (int i = 0; i < avaliableItems.Length; i++)
        {
            for (int ii = 0; ii < avaliableItems[i].scene.Length; ii++)
            {
                if (avaliableItems[i].scene[ii].sceneName == SceneManager.GetActiveScene().name)
                {
                    GameObject item = Instantiate(itemPrefab, avaliableItems[i].scene[ii].spawnPos, Quaternion.identity);
                    item.GetComponent<product>().itemData = avaliableItems[i];
                    item.GetComponent<product>().InitializeData();
                }
            }
        }
    }
}
