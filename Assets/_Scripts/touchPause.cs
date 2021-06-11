using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchPause : MonoBehaviour
{
    public void PauseGame()
    {
        GameObject.Find("Rating Manager").GetComponent<ratingManager>().Pause();
    }
}
