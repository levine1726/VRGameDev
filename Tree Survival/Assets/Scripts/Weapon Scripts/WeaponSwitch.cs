using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WeaponSwitch : MonoBehaviour {

    // public variables
    public GameObject weapon;
    public GameObject hand1Object;
    public GameObject hand2Object;
    public GameObject bowSpawnerWithPedistal;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject weaponSwitchTriggerObject;

    private Hand hand1;
    private Hand hand2;

    private static bool hand1InCollider = false;
    private static bool hand2InCollider = false;



    private GameObject longbow;
    private GameObject arrowHand;
    private ItemPackageSpawner bowPickup;

    private GameObject leftHandGun;
    private GameObject rightHandGun;

    public static bool playerGrabedBow = false;
    private bool bowActive = false;
    private bool weaponReadyToSwitch = false;
    private bool playerLiftedGripsAfterWeaponSwitch = true;

    private bool disableCollisionVibration;
    

    // String tags for use with the GameObject.Find() method
    private string LONGBOW_NAME = "Longbow(Clone)";
    private string ARROW_HAND_NAME = "ArrowHand(Clone)";

    private string STEAMVR_HEAD_NAME = "FollowHead";
    
    
    // Use this for initialization
    void Awake () {
        bowPickup = bowSpawnerWithPedistal.GetComponentInChildren<ItemPackageSpawner>();
        hand1 = hand1Object.GetComponent<Hand>();
        hand2 = hand2Object.GetComponent<Hand>();
        GameObject steamVRHead = GameObject.Find(STEAMVR_HEAD_NAME);
        playerGrabedBow = false;
        GameObject weaponCollider = Instantiate(weaponSwitchTriggerObject, steamVRHead.transform);

        weaponCollider.transform.SetParent(steamVRHead.transform);
    }

    public static void SetHand1InCollider(bool value)
    {
        hand1InCollider = value;
    }

    public static void SetHand2InCollider(bool value)
    {
        hand2InCollider = value;
    }


    private void FixedUpdate () {
		if (bowPickup.justPickedUpItem && bowSpawnerWithPedistal.activeInHierarchy)
        {
            DisableBowSpawner();
            
        }

        if (longbow == null)
        {
            longbow = GameObject.Find(LONGBOW_NAME);
        }
        if (arrowHand == null)
        {
            arrowHand = GameObject.Find(ARROW_HAND_NAME);
        }

        CheckAndSwitchWeapon();

    }

    private void DisableBowSpawner()
    {
        bowSpawnerWithPedistal.SetActive(false);
        playerGrabedBow = true;
        bowActive = true;
        weaponReadyToSwitch = true;
        IntializeLongbowVariables();
    }

    private void IntializeLongbowVariables()
    {
        longbow = GameObject.Find(LONGBOW_NAME);
        arrowHand = GameObject.Find(ARROW_HAND_NAME);

        DontDestroyOnLoad(longbow);
        DontDestroyOnLoad(arrowHand);
    }

    private void SwitchWeapons()
    {
        
        if (bowActive)
        {
            
            longbow.SetActive(false);
            arrowHand.SetActive(false);

            if (gun1 != null )
            {
                
                gun1.SetActive(true);
            }

            if (gun2 != null)
            {
                gun2.SetActive(true);
            }

           
            bowActive = false;
        }
        else
        {
            longbow.SetActive(true);
            arrowHand.SetActive(true);

            gun1.SetActive(false);
            gun2.SetActive(false);
            bowActive = true;
        }
    }

    private void CheckAndSwitchWeapon ()
    {
        if (hand1InCollider && hand2InCollider)
        {
            if (!disableCollisionVibration)
            {

                SwitchReadyVibration();
                StartCoroutine(DisableSwitchingVibration());
            }
            if (hand1.controller.GetPress(SteamVR_Controller.ButtonMask.Grip) && hand2.controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (playerGrabedBow && weaponReadyToSwitch && playerLiftedGripsAfterWeaponSwitch)
                {
                    SwitchWeapons();
                    StartCoroutine(SwitchVibration(0.25f, 400));
                    playerLiftedGripsAfterWeaponSwitch = false;
                    weaponReadyToSwitch = false;
                    StartCoroutine(DisableWeaponSwitchTimer());
                }
            }
        } else
        {
            disableCollisionVibration = false;
        }

        if (hand1.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || hand2.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (!playerLiftedGripsAfterWeaponSwitch)
            {
                playerLiftedGripsAfterWeaponSwitch = true;
            }
        }
    }

    private void SwitchReadyVibration()
    {
        hand1.controller.TriggerHapticPulse(300);
        hand2.controller.TriggerHapticPulse(300);
    }

    IEnumerator SwitchVibration(float timeToVibrate, ushort strength)
    {
        for (float i = 0; i < timeToVibrate; i += Time.deltaTime) 
        {
            hand1.controller.TriggerHapticPulse(strength);
            hand2.controller.TriggerHapticPulse(strength);
            yield return null;
        }
    }

    IEnumerator DisableSwitchingVibration()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        disableCollisionVibration = true;

    }

    IEnumerator DisableWeaponSwitchTimer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        weaponReadyToSwitch = true;
    }
}
