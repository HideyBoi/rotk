using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class itemAccept : MonoBehaviour
{
    public int id;

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
    public int spriteID;

    void Awake()
    {
        items = GameObject.Find("GameManager").GetComponent<GameManager>().avaliableItems;
        rm = GameObject.Find("Rating Manager").GetComponent<ratingManager>();

        StartCoroutine("InnitUI");
    }

    void FixedUpdate()
    {
        if (rm.rating <= 0)
            return;

        time -= Time.fixedDeltaTime;

        if(timeTex != null)
        {
            timeTex.text = Mathf.FloorToInt(time).ToString();
        } else
        {
            return;
        }
            

        if (time < 0)
        {
            Debug.Log("Out of time!");
            Wrong();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (rm.rating <= 0)
            return;

        if (other.CompareTag("Item"))
        {
            if (ItemName == other.transform.Find("ItemAim").GetComponent<product>().itemData.itemName)
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
        rm.ChangeRating(false, selectedItem);
        rm.ChangeScore(false, selectedItem);
        rm.RemoveUISpot(true, id);
        Destroy(Karen);
    }

    void Correct()
    {
        Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        rm.ChangeRating(true, selectedItem);
        rm.ChangeScore(true, selectedItem);
        rm.ChangeQuota();
        rm.RemoveUISpot(false, id);
        rm.KarensServed++;
        Destroy(Karen);
    }

    void GetRandomItem()
    {
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

        if (SceneManager.GetActiveScene().name == "tl1")
        {
            time = selectedItem.scene[0].time;
        }
    }

    IEnumerator InnitUI()
    {
        yield return new WaitForSeconds(0.3f);

        Debug.Log(Karen.GetComponent<karen>().id);
        id = Karen.GetComponent<karen>().id;

        rm.RegisterUISpot(id, Karen.GetComponent<karen>().spriteID);
        timeTex = rm.slots[id].timer;
        itemName = rm.slots[id].itemName;

        GetRandomItem();
    }
}