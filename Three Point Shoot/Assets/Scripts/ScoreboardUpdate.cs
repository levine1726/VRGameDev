using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUpdate : MonoBehaviour {

    private TextMesh scoreText;

	// Use this for initialization
	void Awake () {
        scoreText = GetComponent<TextMesh>();
	}
	
	
    
}
