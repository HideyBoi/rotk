using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt;

public class signInButton : MonoBehaviour
{  
    
    public bool signIn;
    public bool logOut;
    public bool leaderBoard;
    public bool trophies;
    
    public void openSignOnScreen()
    {
        if(signIn)
        {
            GameJolt.UI.GameJoltUI.Instance.ShowSignIn();
        }
        if(logOut)
        {
            var isSignedIn = GameJolt.API.GameJoltAPI.Instance.CurrentUser != null;
            if (isSignedIn)
            {
                GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignOut();
            }
        }
        if(leaderBoard)
        {
            GameJolt.UI.GameJoltUI.Instance.ShowLeaderboards();
        }
        if(trophies)
        {
            GameJolt.UI.GameJoltUI.Instance.ShowTrophies();
        }
        
    }
}
