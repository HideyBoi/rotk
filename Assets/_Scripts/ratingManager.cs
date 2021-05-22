using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJolt;
using UnityEngine.SceneManagement;

public class ratingManager : MonoBehaviour
{
    public float rating;
    public GameObject notice;
    public float score;
    public float HighScore;
    public Sprite emptyPortrait;
    public Sprite[] portraits;
    public TextMeshProUGUI HS;
    public TextMeshProUGUI S;
    public TextMeshProUGUI gameoverText;
    public KarenStatusSlot[] slots;
    [TextArea]
    public string ratingGameover;
    [TextArea]
    public string robberGameover;
    [TextArea]
    public string quotaGameover;


    void Update()
    {
        if (rating >= 10)
        {
            rating = 10;
        }

        if (rating <= 0)
        {
            GameOver(0);
        }

        HS.text = HighScore.ToString();
        S.text = score.ToString();
    }

    public void ChangeRating(bool correct, Item itemData)
    {
        int toChange = 0;

        for (int i = 0; i < itemData.scene.Length; i++)
        {
            if (itemData.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                if (correct)
                {
                    toChange = itemData.scene[i].score;
                } else
                {
                    toChange = itemData.scene[i].incorrectScore * -1;
                }
            }
        }

        rating += toChange;
    }

    public void ChangeScore(bool correct, Item itemData)
    {
        int toChange = 0;

        for (int i = 0; i < itemData.scene.Length; i++)
        {
            if (itemData.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                if (correct)
                {
                    toChange = itemData.scene[i].score * 100;
                }
                else
                {
                    toChange = itemData.scene[i].incorrectScore * -100;
                }
            }
        }

        score += toChange;
    }

    void GameOver(int reason)
    {
        notice.SetActive(true);

        string reasonForGameover = "[NO ENTRY FOR SPECIFIED GAMEOVER ID!]";

        switch (reason)
        {
            case 0:
                reasonForGameover = ratingGameover;
                break;
            case 1:
                reasonForGameover = robberGameover;
                break;
            case 2:
                reasonForGameover = quotaGameover;
                break;
        }

        gameoverText.text = reasonForGameover;

        GameObject player = GameObject.Find("Player");
        Destroy(player);
    }

    public void RemoveUISpot(bool isBadReason, int karenId)
    {
        slots[karenId].portrait.sprite = emptyPortrait;
        slots[karenId].itemName.text = "-";
        slots[karenId].timer.text = "-";
    }

    public void RegisterUISpot(int karenId, int spriteId)
    {
        slots[karenId].portrait.sprite = portraits[spriteId];
    }
}
