using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnableNavigation : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("Landed", true);
            anim.SetBool("Walking", true);
            GetComponent<MonsterNavigation>().enabled = true;
            this.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
