using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchInputToggle : MonoBehaviour
{

    public Toggle toggle;
    public int saveVarible;
    public int loadVarible;
    public bool isEnabled;

    void Awake()
    {
        loadVarible = PlayerPrefs.GetInt("usingTouchControls");

        if (loadVarible == 1)
        {
            isEnabled = true;
        }else
        {
            isEnabled = false;
        }

        toggle.isOn = isEnabled;
    }


    void Update()
    {
        if(toggle.isOn)
        {
            saveVarible = 1;
        }else
        {
            saveVarible = 0;
        }


        PlayerPrefs.SetInt("usingTouchControls", saveVarible);
    }
}
