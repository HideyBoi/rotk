using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscordManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private Discord.Discord discord;

    public DiscordScene[] scenes;

    void Awake()
    {
        string details = "[NOT SET]";

        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].name == SceneManager.GetActiveScene().name)
            {
                if (scenes[i].levelType == DiscordScene.LevelType.MENU)
                {
                    details = $"Currently in the menus!";
                } else
                {
                    string gameMode = "Arcade";

                    details = $"Playing {gameMode} on {scenes[i].displayName}";
                }
            }
        }

        //creates an instance of discord
        discord = new Discord.Discord(839323469808009246, (System.UInt64)Discord.CreateFlags.Default);
        //gets the activity manager
        var activityManager = discord.GetActivityManager();
        //creates an activity
        string det = "Playing version: " + Application.version;
#if UNITY_EDITOR_WIN
        det = "Developing the game!";
        Debug.Log("Version : " + Application.version);
#endif

        var activity = new Discord.Activity
        {
            Details = det,
            State = details,
            Assets =
            {
                LargeImage = "main", // Larger Image Asset Key
                LargeText = ":3 No secrets here.", // Large Image Tooltip
            }
        };
        //updates the activity
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
                Debug.Log("Status Set!");
            else
                Debug.LogError("Something happened while updating activity...");
        });
    }

    void Update()
    {
        discord.RunCallbacks();
    }

    void OnDisable()
    {
        //same as first one
        var activityManager = discord.GetActivityManager();
        //removes activities
        activityManager.ClearActivity((res) => {
            if (res == Discord.Result.Ok)
                Debug.Log("Status removed!");
            else
                Debug.LogError("Something happened while clearing activity...");
        });

        //pushes changes to discord
        discord.RunCallbacks();

        discord.Dispose();
    }
#endif
}
