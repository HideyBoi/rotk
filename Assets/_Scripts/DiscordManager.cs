using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordManager : MonoBehaviour
{
    public Discord.Discord discord;

    void Awake()
    {
        //creates an instance of discord
        discord = new Discord.Discord(839323469808009246, (System.UInt64)Discord.CreateFlags.Default);
        //gets the activity manager
        var activityManager = discord.GetActivityManager();
        //creates an activity
        var activity = new Discord.Activity
        {
            Details = "Developing the game?",
            State = "Playing Arcade!",
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

    private void OnApplicationQuit()
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

        
    }
}
