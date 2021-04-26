using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class levelSelect : MonoBehaviour
{
    public SceneData scene;

    public Image spriteR;
    public TextMeshProUGUI levelName;

    public loadingScreen ls;

    void Awake()
    {
        levelName.text = scene.displayName;
        spriteR.sprite = scene.sceneIcon;
    }

    public void Clicked()
    {
        ls.loadScene(scene.sceneName);
    }
}
