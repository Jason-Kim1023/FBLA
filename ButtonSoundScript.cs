using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSoundScript : MonoBehaviour
{
    //Creates a public AudioSource that can be changed in Unity  (What is going to be playing the sound)
    public AudioSource myFx;
    //Creates a public AudioClip that can be changed in Unity   (What the sound is)
    public AudioClip hoverFx;


    //Creates a public void that I could input a sound into... Allows me that set up in unity so that when a cursor hovers 
    //the button, the sound will be made.
    public void HoverSound()
    {
        //Means that it plays once when hovers... does not loop forever
        myFx.PlayOneShot(hoverFx);
    }

}
