using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour
{
    
    public ratingManager RatingManager;
    public float showEmptyStarAt;
    public float showHalfStarAt;
    public float showFullStarAt;
    public Sprite halfStar;
    public Sprite fullStar;
    public Sprite emptyStar;

    public Image image;
    
    void Update()
    {
        if(RatingManager.rating == showHalfStarAt)
        {
            image.sprite = halfStar;
        }

        if(RatingManager.rating >= showFullStarAt)
        {
            image.sprite = fullStar;
        }
        
        if(RatingManager.rating <= showEmptyStarAt)
        {
            image.sprite = emptyStar;
        }
    }
}
