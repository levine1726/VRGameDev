using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMusic : MonoBehaviour {

    private AudioSource audioSource;
    private bool songNotStarted;

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
        songNotStarted = true;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log("playerGrabedBow is " + WeaponSwitch.playerGrabedBow);
		if (WeaponSwitch.playerGrabedBow && songNotStarted)
        {
            audioSource.Play();
            songNotStarted = false;
        }
	}
}
