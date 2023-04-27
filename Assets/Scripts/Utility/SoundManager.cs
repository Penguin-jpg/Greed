using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource slashSource;
    public AudioSource biteSource;
    public AudioSource deathSource;
    public AudioSource hitSource;
    public AudioSource healSource;
    private static Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        sounds["slash"] = slashSource;
        sounds["bite"] = biteSource;
        sounds["death"] = deathSource;
        sounds["hit"] = hitSource;
        sounds["heal"] = healSource;
    }

    public static void PlaySound(string source)
    {
        if(sounds.ContainsKey(source))
        { 
            sounds[source].Play();
        }
    }
}
