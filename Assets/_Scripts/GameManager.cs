using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Item[] avaliableItems;

    public SceneData currentStage;

    public GameObject itemPrefab;

    public AudioSource audioSource;

    void Start()
    {
        avaliableItems = currentStage.items;

        audioSource.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME");

        audioSource.clip = currentStage.music;

        audioSource.Play();
    }

    public void ChangeVol()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME");
    }
}
