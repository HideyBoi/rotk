using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiscordScene
{
    public string name = "";
    public string displayName = "[NAME NOT SET]";
    public enum LevelType {  MENU, GAME  };
    public LevelType levelType;
}
