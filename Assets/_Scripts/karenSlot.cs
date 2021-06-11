using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karenSlot : MonoBehaviour
{
    public GameManager gameManager;

    public int id;

    public GameObject karenPrefab; 
    public GameObject robberPrefab; 
    public GameObject Karen;

    public float karenTime;

    public float maxTime;
    public float minTime;

    public bool isTimeSet;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        maxTime = gameManager.currentStage.maxKarenSpawnTime;
        minTime = gameManager.currentStage.minKarenSpawnTime;

        karenTime = Random.Range(minTime, maxTime);
        isTimeSet = true;
    }
    
    void Update()
    {
        karenTime -= Time.deltaTime;

        if (Karen == null && karenTime < 0 && !isTimeSet)
        {
            karenTime = Random.Range(minTime, maxTime);
            isTimeSet = true;
        }

        if (Karen == null && karenTime < 0 && isTimeSet)
        {
            int rng = Random.Range(0, 50);

            switch(rng == 1)
            {
                case true:
                    SpawnRobber();
                    break;
                case false:
                    SpawnKaren();
                    break;
            }

            isTimeSet = false;
        }
    }   
    
    void SpawnKaren()
    {
        Karen = Instantiate(karenPrefab, transform.position, transform.rotation);
        Karen.GetComponent<karen>().id = id;
        Debug.Log(Karen.GetComponent<karen>());
    }

    void SpawnRobber()
    {
        Karen = Instantiate(robberPrefab, transform.position, transform.rotation);
        Karen.GetComponent<Robber>().id = id;
        Debug.Log(Karen.GetComponent<Robber>());
    }
}
