using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAwake : MonoBehaviour
{
    public string sound;

    void Awake()
    {
        AudioManager.instance.Play(sound);
    }
}
