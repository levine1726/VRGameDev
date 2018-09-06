using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealthManager : MonoBehaviour {

    private Text healthText;
    private TreeHealth tree;

	// Use this for initialization
	void Awake () {
        healthText = GameObject.Find("HealthUI").GetComponent<Text>();
        healthText.enabled = false;
        tree = null;
	}

    private void FixedUpdate()
    {
        if (tree != null)
        {
            healthText.text = "Tree Health: " + tree.GetHealth();
        }
        
       
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Tree")
        {
            tree = collision.GetComponent<TreeHealth>();
            healthText.enabled = true;
            healthText.text = "Tree Health: " + tree.GetHealth();

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "collision")
        {
            healthText.enabled = false;
            tree = null;

        }
    }

}
