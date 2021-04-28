using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimable : MonoBehaviour
{
    public GameObject GUI;
    public SpriteRenderer spriteRenderer;
    public Material defaultMat;
    public Material outlineMat;

    public void OnAim() 
    {
        GUI.SetActive(true);
        spriteRenderer.material = outlineMat;
    }

    public void OnStopAim()
    {
        GUI.SetActive(false);
        spriteRenderer.material = defaultMat;
    }
}
