using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ObjectPickupAndThrow : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller;

    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    //update is called once per frame
    void FixedUpdate()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index);
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && !ScoreController.countDownStarted)
        {
            Debug.Log("grip pressed");
            ScoreController.StartGame();
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if (ScoreController.gameStarted && !ScoreController.gameEnded)
        {

            if (col.gameObject.tag == "Ball" && controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {

                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
            }
            if (col.gameObject.tag == "Ball" && controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                throwBall(col);
            }

        }
    }

    private void throwBall(Collider col)
    {
        col.gameObject.transform.SetParent(null);
        col.attachedRigidbody.isKinematic = false;

        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            col.attachedRigidbody.velocity = origin.TransformVector(controller.velocity);
            col.attachedRigidbody.angularVelocity = origin.TransformVector(controller.angularVelocity);
        }
        else
        {
            col.attachedRigidbody.velocity = controller.velocity;
            col.attachedRigidbody.angularVelocity = controller.angularVelocity;
        }
    }


}
