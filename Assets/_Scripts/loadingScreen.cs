using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour
{
    public Animator loadingScreenAnimator;

    public void loadScene(string sceneName)
    {
        loadingScreenAnimator.SetBool("Loading", true);

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void loadSameScene()
    {
        loadingScreenAnimator.SetBool("Loading", true);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
