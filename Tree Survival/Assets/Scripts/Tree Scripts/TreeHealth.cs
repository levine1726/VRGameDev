using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeHealth : MonoBehaviour {

    private float health = 100f;
    private bool isPlayerOnTree;
    private bool treeDestroyed;
    private Rigidbody playerRb;

    public static bool playerLeftGround = false;
   

    private void Awake()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            playerRb.isKinematic = false;
            
        }
    }

    public bool GetIsPlayerOnTree()
    {
        return isPlayerOnTree;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage (float damage)
    {
        health -= damage;
       
    }

    private void OnTriggerEnter(Collider other)
    { 

        if (other.name == "Player")
        {
            
            isPlayerOnTree = true;
            playerLeftGround = true;

            CheckPlatform();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            isPlayerOnTree = false;
        }
    }

    private void CheckPlatform()
    {
        if (PlatformDeactivationScript.platformActive == true)
        {
            PlatformDeactivationScript stand = GameObject.Find("SpawningStand").GetComponent<PlatformDeactivationScript>();
            if (stand != null)
            {
                stand.DestroyPlatform();
            }
            
        }
    }


}
