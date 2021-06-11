using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlButton : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
