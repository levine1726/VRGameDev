using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformDeactivationScript : MonoBehaviour {

    private Text countdownText;
    public int timeUntillPlatformIsDestroyed = 10;
    public static bool platformActive;
    private bool countdownNotStarted = true;

    private Rigidbody rb;

    private void Awake()
    {
        countdownText = GameObject.Find("CountdownUI").GetComponent<Text>();
        countdownText.enabled = false;
        platformActive = true;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (WeaponSwitch.playerGrabedBow && countdownNotStarted)
        {
            
            countdownText.enabled = true;
            StartCoroutine(DisablePlatform(timeUntillPlatformIsDestroyed));
        }
	}

    IEnumerator DisablePlatform(int timeUntilPlatformDestroy)
    {
        countdownNotStarted = false;
        for (int time = timeUntillPlatformIsDestroyed; 0 < time; time--)
        {
            countdownText.text = "WARNING: YOU HAVE " + time + " SECONDS TO MOVE!!";
            yield return new WaitForSecondsRealtime(1f);
        }

        platformActive = false;
        countdownText.enabled = false;
        DestroyPlatform();

    }

    public void DestroyPlatform()
    {
         
        StopCoroutine("DisablePlatform");
        rb.isKinematic = false;
        countdownText.enabled = false;
        Destroy(gameObject);
        
    }
}
