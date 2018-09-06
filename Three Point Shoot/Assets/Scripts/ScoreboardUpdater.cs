using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUpdater : MonoBehaviour {

    private TextMesh scoreText;

	// Use this for initialization
	void Awake () {
        scoreText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
