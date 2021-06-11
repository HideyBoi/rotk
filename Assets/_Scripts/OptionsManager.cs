using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Volume")]
    public Slider volume;
    public Slider musicVolume;
    [Header("Render Quality")]
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown qualityDropdown;

    public void Start()
    {
        if (PlayerPrefs.HasKey("VOLUME"))
        {
            volume.value = PlayerPrefs.GetFloat("VOLUME");
        } else
        {
            PlayerPrefs.SetFloat("VOLUME", 1);
        }

        if (PlayerPrefs.HasKey("QUALITY_LEVEL"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QUALITY_LEVEL");
            ChangeRenderQuality(PlayerPrefs.GetInt("QUALITY_LEVEL"), false);
        } else
        {
            PlayerPrefs.SetFloat("QUALITY_LEVEL", QualitySettings.GetQualityLevel());
        }

        if (PlayerPrefs.HasKey("MUSIC_VOLUME"))
        {
            musicVolume.value = PlayerPrefs.GetFloat("MUSIC_VOLUME");
        } else
        {
            PlayerPrefs.SetFloat("MUSIC_VOLUME", 1);
        }
    }

    public void ChangeRenderQuality(int value, bool isDropdown)
    {
        int level;

        if (!isDropdown)
        {
            level = value;
        } else
        {
            level = qualityDropdown.value;
        } 
        
        QualitySettings.SetQualityLevel(level);
        QualitySettings.renderPipeline = qualityLevels[level];

        PlayerPrefs.SetInt("QUALITY_LEVEL", QualitySettings.GetQualityLevel());
    }

    public void ChangeRenderQuality()
    {
        int level = qualityDropdown.value;

        QualitySettings.SetQualityLevel(level);
        QualitySettings.renderPipeline = qualityLevels[level];

        PlayerPrefs.SetInt("QUALITY_LEVEL", QualitySettings.GetQualityLevel());
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("VOLUME", volume.value);
    }

    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("MUSIC_VOLUME", musicVolume.value);

        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            gm.GetComponent<GameManager>().ChangeVol();
        }
    }

    public void SignInToGJ()
    {
        GameJolt.UI.GameJoltUI.Instance.ShowSignIn(
            (bool signInSuccess) => {
                Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
            },
            (bool userFetchedSuccess) => {
                Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
            }
        );
    }

    public void SignOutOfGJ()
    {
        var isSignedIn = GameJolt.API.GameJoltAPI.Instance.CurrentUser != null;
        if (isSignedIn)
        {
            GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignOut();
        }
    }
}
