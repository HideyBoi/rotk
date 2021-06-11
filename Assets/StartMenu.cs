using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameJolt;

public class StartMenu : MonoBehaviour
{
    public GameObject buttons;
    public GameObject howToPlay;

    public TextMeshProUGUI signedInText;

    void Update()
    {
        if (GameJolt.API.GameJoltAPI.Instance.HasSignedInUser)
        {
            signedInText.text = "Signed in!";
        } else
        {
            signedInText.text = "Signing into GameJolt will allow you to earn trophies, get onto the leaderboard, and save your highscore online!\n See <color=\"red\">options";
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("FIRST_RUN"))
        {
            buttons.SetActive(false);
            howToPlay.SetActive(true);

            PlayerPrefs.SetInt("FIRST_RUN", 1);
        }

        PlayerPrefs.SetString("_!HEY_YOU!", "Don't spoil the game or change your score. " +
            "That's a rotten no good thing to do. You're only cheating yourself. You're a fraud." +
            "So please, just turn around and go back.");
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

    public void BREAKLEG()
    {
        GameJolt.UI.GameJoltUI.Instance.ShowLeaderboards();
    }

    public void TOGETCOPPAH()
    {
        GameJolt.UI.GameJoltUI.Instance.ShowTrophies();
    }
}
