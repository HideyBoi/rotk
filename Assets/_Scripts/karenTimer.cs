using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class karenTimer : MonoBehaviour
{
    public float timeLeft = 30.0f;
    public itemAccept ItemAccept;
    
    public TextMeshProUGUI tmp;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        tmp.text = timeLeft.ToString();
        if(timeLeft < 0)
        {
            Debug.Log("Out of time.");
        }
    }
}
