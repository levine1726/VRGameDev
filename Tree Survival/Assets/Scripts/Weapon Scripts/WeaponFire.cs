using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WeaponFire : MonoBehaviour {

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public GameObject bulletManager;
    public AudioClip gunshotClip;

    private AudioSource audioSource;
    private Transform bulletSpawnLocation;
    private Hand hand;
    private bool isGunReadyToFire = true; // used to create a firing 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void OnEnable() {
        hand = GetComponentInParent<Hand>();
        bulletSpawnLocation = GetComponentInChildren<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && isGunReadyToFire)
        {
            FireBullet();
        }
	}

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.transform.SetParent(bulletManager.transform);
        bulletRb.velocity = bulletSpawnLocation.forward * bulletSpeed;
        audioSource.PlayOneShot(gunshotClip);

        StartCoroutine(FireVibration(0.05f));
    }

    IEnumerator FireVibration(float timeToVibrate)
    {
        for (float i = 0; i < timeToVibrate; i += Time.deltaTime)
        {
            hand.controller.TriggerHapticPulse(500);
            yield return null;
        }
    }
}
