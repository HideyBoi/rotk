using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karenSlot : MonoBehaviour
{
    public int id;

    public GameObject karenPrefab; 
    public GameObject Karen;

    public float karenTime;

    public float maxTime;
    public float minTime;

    public bool isTimeSet;

    void Start()
    {
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
            Karen = Instantiate(karenPrefab, transform.position, transform.rotation);
            Karen.GetComponent<karen>().id = id;
            Debug.Log(Karen.GetComponent<karen>());
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
