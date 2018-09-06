using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    
    private bool hasBallLeftPlayArea = false;

    private bool firstQuadDectection = false;
    private bool secondQuadDectection = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if ((ScoreController.gameStarted && !ScoreController.gameEnded) || ScoreController.gameEnded)
        {
            EnableBallPhysics(false);
        }
        else
        { 
            EnableBallPhysics(true);
            
        }
        if (hasBallLeftPlayArea)
        {
            BallDeletionCheck();
        }

        if (ScoreController.gameTimer < 0f)
        {
            Destroy(gameObject);
        }

    }

    private void EnableBallPhysics(bool boolean)
    {
        if (boolean)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        } else
        {
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        }
       
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("OnTriggerEnter activated for" + name);
        if (trigger.name == "Top Trigger" && !secondQuadDectection)
        {
            Debug.Log("Detected Top Trigger");
            firstQuadDectection = true;
        }

        if (trigger.name == "Bottom Trigger" && !firstQuadDectection)
        {
            secondQuadDectection = true;
            Debug.Log("Bottom Trigger hit first, do not score this ball");
        }

        if (trigger.name == "Bottom Trigger" && firstQuadDectection)
        {
            Debug.Log("Score!");
            ScoreController.IncrementScore();
        }

        if (trigger.name == "[CameraRig]")
        {
            Debug.Log("Ball has left largest play area");
            hasBallLeftPlayArea = true;
        }
    }



    private void BallDeletionCheck()
    {
        if (rb.velocity.Equals(Vector3.zero) && rb.angularVelocity.Equals(Vector3.zero))
        {
            gameObject.SetActive(false);
        }
    }

    

}
