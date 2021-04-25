using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt;
using TMPro;

public class areyousignedin : MonoBehaviour
{

    public TextMeshProUGUI tmp;
    public Color signedOutColor;
    public Color signedInColor;

    void LateUpdate()
    {
        bool isSignedIn = GameJolt.API.GameJoltAPI.Instance.CurrentUser != null;

        if (isSignedIn)
        {
            tmp.text = "You are signed in! Your progress will be saved!";
            tmp.color = signedInColor;
        }
        else
        {
            tmp.text = "You are not signed in! Sign in to save your highscores and progress!";
            tmp.color = signedOutColor;
        }
    }

}
