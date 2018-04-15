using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioClip SoundToPlay;
	public float volume;
	AudioSource audio;
	public bool alreadyPlayed = false;	

	void Start(){
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(){
		alreadyPlayed = true;
		play();
	}

	void OnTriggerExit(){
		alreadyPlayed = false;
		audio.Stop ();
	}

	void play(){
		audio.Play();
		//audio.Loop();	
		//audio.PlayOneShot (SoundToPlay, volume);
	}
}