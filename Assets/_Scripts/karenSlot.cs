using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karenSlot : MonoBehaviour
{
    
    public GameObject karenPrefab; 
    public bool noKaren;
    public GameObject karen;
    

    void Start()
    {
        karen = Instantiate(karenPrefab, transform.position, transform.rotation);
    }
    
    void Update()
    {
        if(karen != null)
        {
            noKaren = true;
        } 
        else
        {
            noKaren = false;
        }

        if (!noKaren)
        {
            karen = Instantiate(karenPrefab, transform.position, transform.rotation);
        }
    }       
        

}
