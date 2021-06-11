using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "SceneData", order = 1)]
public class SceneData : ScriptableObject
{
    [Header("Display")]
    public Sprite sceneIcon;
    public string displayName;
    [Header("Game and systems")]
    public string sceneName;
    public Item[] items;
    public AudioClip music;
    public float maxKarenSpawnTime;
    public float minKarenSpawnTime;
    public float maxQuotaTime;
    public float minQuotaTime;
    public int maxQuota;
    public int minQuota;
}
