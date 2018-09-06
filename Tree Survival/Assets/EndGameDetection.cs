using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class EndGameDetection : MonoBehaviour {

    private Text endGameText;

    public static bool endgameTriggered = false;

    public GameObject hand1Object;
    public GameObject hand2Object;

    private Hand hand1;
    private Hand hand2;


    private void Awake()
    {
        endGameText = GameObject.Find("GameOverUI").GetComponent<Text>();
        hand1 = hand1Object.GetComponent<Hand>();
        hand2 = hand2Object.GetComponent<Hand>();
        endgameTriggered = false;
    }

    private void FixedUpdate()
    {

        Debug.Log("playerLeftGround is " + TreeHealth.playerLeftGround);

        if (endgameTriggered) {
            if (hand1.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || hand2.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }

 

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision is " + collision.gameObject.tag);
        
            if (collision.gameObject.tag == "Floor")
            {
                EndGame();
            }
        
    }

    private void EndGame()
    {

        GameObject.Find("HealthUI").GetComponent<Text>().enabled = false;

        //Destroy all monsters
        endgameTriggered = true;
        endGameText.enabled = true;
       
    }



}
