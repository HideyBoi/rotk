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
}
