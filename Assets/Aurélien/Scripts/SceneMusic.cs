using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    //public AudioClip newMusic; //Pick an audio track to play.

    public void Awake()
    {
        //newmusic = clip avec le nom de la scene en cours dans l'audiomanager;

        Sound newMusic = Array.Find(AudioManager.instance.sounds, s => s.name.EndsWith(SceneManager.GetActiveScene().name));

        if (newMusic != null && !newMusic.source.isPlaying)
        {
            Debug.Log("Music " + newMusic.name + " found in AudioManager"); 
            AudioManager.instance.Play(newMusic.name);
        }
        else
        {
            Debug.Log("Music not found in AudioManager");
        }

        /*var go = GameObject.Find("GameMusic"); //Finds the game object called Game Music, if it goes by a different name, change this.
        if (go)
        {
            AudioClip oldMusic = go.GetComponent<AudioSource>().clip;
            if (oldMusic != newMusic)
            {
                go.GetComponent<AudioSource>().clip = newMusic; //Replaces the old audio with the new one set in the inspector.
                go.GetComponent<AudioSource>().Play(); //Plays the audio.
            }
        }*/
    }
}
