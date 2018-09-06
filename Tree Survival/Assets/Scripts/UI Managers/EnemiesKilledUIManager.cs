using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesKilledUIManager : MonoBehaviour {

    
    private Text enemiesKilledText;

    private void Awake()
    {
        enemiesKilledText = GetComponent<Text>();
    }

    // Update is called once per frame
    private void FixedUpdate () {
        enemiesKilledText.text = "Enemies Destroyed: " + TerrainGenerator.monstersKilled;
	}

}
