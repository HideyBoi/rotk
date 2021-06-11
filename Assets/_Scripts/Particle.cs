using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public AudioSource audioComponet;

    void Awake()
    {
        audioComponet.volume = PlayerPrefs.GetFloat("VOLUME");
    }

    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
