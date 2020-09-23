using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
    //Implements the AudioMixer function and allows music to be edited
    public AudioMixer audioMixer;
    //Makes the volume adjustable
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("BackGroundVolume", volume);
    }
}
