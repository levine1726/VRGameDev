using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        StartCoroutine(BulletTerminationCountdown());
	}
	
	IEnumerator BulletTerminationCountdown()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
