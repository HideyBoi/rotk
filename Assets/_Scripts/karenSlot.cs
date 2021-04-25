using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karenSlot : MonoBehaviour
{
    
    public GameObject karenPrefab; 
    public GameObject karen;

    public float karenTime;

    public float maxTime;
    public float minTime;

    public bool isTimeSet;

    void Start()
    {
        karen = Instantiate(karenPrefab, transform.position, transform.rotation);
    }
    
    void Update()
    {
        karenTime -= Time.deltaTime;

        if (karen == null && karenTime < 0 && !isTimeSet)
        {
            karenTime = Random.Range(minTime, maxTime);
            isTimeSet = true;
        }

        if (karen == null && karenTime < 0 && isTimeSet)
        {
            karen = Instantiate(karenPrefab, transform.position, transform.rotation);
            isTimeSet = false;
        }
        
        /*
        if (karen == null)
        {
            karen = Instantiate(karenPrefab, transform.position, transform.rotation);
        }
        */
    }       
}
