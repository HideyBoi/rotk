using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class loadingScreen : MonoBehaviour
{
    public Animator loadingScreenAnimator;
    public Animator blackFadeIn;

    [TextArea]
    public string[] tips;
    public TextMeshProUGUI tipsTextObj;
    public Sprite[] backgrounds;
    public Image image;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (PlayerPrefs.GetInt("FROM_GAME") != 1)
            {
                loadingScreenAnimator.SetBool("isGame", false);
                blackFadeIn.SetBool("isMenu", true);
            }
            PlayerPrefs.SetInt("FROM_GAME", 0);
        }
            

        tipsTextObj.text = tips[PlayerPrefs.GetInt("LAST_TIP")].ToString();

        image.sprite = backgrounds[PlayerPrefs.GetInt("LAST_SPRITE")];
    }

    public void loadScene(string sceneName)
    {
        loadingScreenAnimator.SetBool("Loading", true);

        GenDesign();

        StartCoroutine("LoadS", sceneName);
    }

    public void loadSameScene()
    {
        loadingScreenAnimator.SetBool("Loading", true);

        GenDesign();

        StartCoroutine("LoadS", SceneManager.GetActiveScene().name);
    }

    void GenDesign()
    {
        int tipRNG = Random.Range(0, tips.Length);
        int imgRNG = Random.Range(0, backgrounds.Length);

        tipsTextObj.text = tips[tipRNG].ToString();

        image.sprite = backgrounds[imgRNG];

        PlayerPrefs.SetInt("LAST_TIP", tipRNG);
        PlayerPrefs.SetInt("LAST_SPRITE", imgRNG);
    }

    IEnumerator LoadS(string scene)
    {
        yield return new WaitForSeconds(3f);

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            PlayerPrefs.SetInt("FROM_GAME", 1);
        }

        SceneManager.LoadSceneAsync(scene);
    }
}
