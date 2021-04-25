using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    
    public string sceneName;

    public Animator loadingscreenanimation;
    
    public void load()
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
