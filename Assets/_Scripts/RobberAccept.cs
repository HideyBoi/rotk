using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RobberAccept : MonoBehaviour
{
    public int id;

    public GameObject robber;

    public ratingManager rm;
    public GameObject wrongEffectPrefab;
    public GameObject correctEffectPrefab; 

    public TextMeshProUGUI timeTex;
    public float time;
    public float timeMax;
    public float timeMin;
    public TextMeshProUGUI itemName;
    private Color nameCol = Color.red;
    public string ItemName;
    public int spriteID;

    void Awake()
    {
        rm = GameObject.Find("Rating Manager").GetComponent<ratingManager>();

        StartCoroutine("InnitUI");
    }

    void FixedUpdate()
    {
        if (rm.rating <= 0)
            return;

        time -= Time.fixedDeltaTime;

        if (timeTex != null)
        {
            timeTex.text = Mathf.FloorToInt(time).ToString();
        }
        else
        {
            return;
        }


        if (time < 0)
        {
            Debug.Log("Out of time! You loose!");
            Failure();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (rm.rating <= 0)
            return;

        if (other.CompareTag("Player"))
        {
            KickOut();
        }
    }

    void KickOut()
    {
        Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        rm.ChangeScore(100);
        rm.RemoveUISpot(false, id);
        Destroy(robber);
    }

    void Failure()
    {
        Instantiate(wrongEffectPrefab, transform.position, Quaternion.identity);
        rm.ChangeScore(-100);
        rm.RemoveUISpot(true, id);
        rm.GameOver(1);
        Destroy(robber);
    }

    IEnumerator InnitUI()
    {
        yield return new WaitForSeconds(0.3f);

        Debug.Log(robber.GetComponent<Robber>().id);
        id = robber.GetComponent<Robber>().id;

        rm.RegisterUISpot(id);
        timeTex = rm.slots[id].timer;
        itemName = rm.slots[id].itemName;

        itemName.text = "<color=#fc4903>Robber!";

        time = Random.Range(timeMin, timeMax);
    }
}