using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class levelIcon : MonoBehaviour
{
    public string levelName;
    public Sprite levelSprite;
    public TextMeshProUGUI levelNameText;
    public Image levelSpriteRender;
    public string sceneName;
    public Animator loadingscreenanimation;


    void Awake()
    {
        levelNameText.text = levelName;
        levelSpriteRender.sprite = levelSprite;
    }

    public void loadLevel()
    {
        StartCoroutine("loading");
    }

    IEnumerator loading()
    {
        loadingscreenanimation.SetBool("showscreen", true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);
    }
}
