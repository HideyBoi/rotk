using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameJolt;

public class ratingManager : MonoBehaviour
{
    public float rating;
    public GameObject notice;
    public float score;
    public float HighScore;
    public TextMeshProUGUI HS;
    public TextMeshProUGUI S;

    void Update()
    {
        if (rating >= 10)
        {
            rating = 10;
        }

        if (rating <= 0)
        {
            notice.SetActive(true);
            GameObject player = GameObject.Find("Player");
            Destroy(player);
        }

        HS.text = HighScore.ToString();
        S.text = score.ToString();
    }
}
