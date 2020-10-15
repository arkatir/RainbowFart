using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip newMusic; //Pick an audio track to play.

    public void Awake()
    {
        var go = GameObject.Find("GameMusic"); //Finds the game object called Game Music, if it goes by a different name, change this.
        AudioClip oldMusic = go.GetComponent<AudioSource>().clip;
        if(oldMusic != newMusic)
        {
            go.GetComponent<AudioSource>().clip = newMusic; //Replaces the old audio with the new one set in the inspector.
            go.GetComponent<AudioSource>().Play(); //Plays the audio.
        }
    }
}
