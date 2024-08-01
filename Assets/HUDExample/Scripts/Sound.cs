using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume; // Volumen por defecto a 1 (máximo)
    public float pitch;  // Pitch por defecto a 1 (normal)
}
