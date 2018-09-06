using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowInstanitateTest : MonoBehaviour {

    public GameObject bowPrefab;
    public GameObject arrowHandPrefab;
    public GameObject hand1;
    public GameObject hand2;

	// Use this for initialization
	void Awake () {
        Instantiate(bowPrefab, hand1.transform);
        Instantiate(arrowHandPrefab, hand2.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
