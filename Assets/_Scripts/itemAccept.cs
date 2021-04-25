using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt;

public class itemAccept : MonoBehaviour
{
    public GameObject productPrefab;
    public ratingManager rm;
    public GameObject wrongEffectPrefab;
    public GameObject correctEffectPrefab;
    public float KarenCurrent;
    public float KarenNew;

    void Awake()
    {
        rm = GameObject.Find("Rating Manager").GetComponent<ratingManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }
}