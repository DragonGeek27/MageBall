using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioController : MonoBehaviour {
    private AudioSource audioSource;
	// Use this for initialization
	void Awake () {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1.0f + Random.Range(-.30f, .1f);
        audioSource.volume = 1.0f - Random.Range(0.0f, .25f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
