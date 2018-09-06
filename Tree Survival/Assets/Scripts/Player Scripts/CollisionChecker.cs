using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeaponTrigger")
        {
            if (this.name == "Hand1")
            {
                WeaponSwitch.SetHand1InCollider(true);
            }
            else if (this.name == "Hand2")
            {
                WeaponSwitch.SetHand2InCollider(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WeaponTrigger")
        {
            if (this.name == "Hand1")
            {
                WeaponSwitch.SetHand1InCollider(false);
            }
            else if (this.name == "Hand2")
            {
                WeaponSwitch.SetHand2InCollider(false);
            }
        }
    }
}
