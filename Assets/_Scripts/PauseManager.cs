using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public loadingScreen ls;

    public void Resume()
    {
        Time.timeScale = 1;

        GameObject.Find("Player").GetComponent<ItemManager>().paused = false;

        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        ls.loadScene("Main Menu");
        Time.timeScale = 1;
    }
}
