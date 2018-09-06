using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class MonsterNavigation : MonoBehaviour {

    
    private Transform playerLocation;
    private NavMeshAgent agent;
    private bool hasMonsterHitGroundAfterSpawn;
    private Rigidbody rb;
    private GroundMonsterHealthDamage monsterAttackLogic;

	// Use this for initialization
	void Awake () {
        playerLocation = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        monsterAttackLogic = GetComponent<GroundMonsterHealthDamage>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!monsterAttackLogic.destructionSignaled)
        {
            if (monsterAttackLogic.readyToAttack)
            {
                Debug.Log("ReadyToATtack is true");
                if (monsterAttackLogic.treeToAttack != null)
                {
                    agent.destination = monsterAttackLogic.treeToAttack.transform.position;
                }
                else
                {
                    agent.destination = playerLocation.transform.position;
                }

            }
            else
            {
                Debug.Log("Moving to player position");
                agent.destination = playerLocation.transform.position;
            }
        } else
        {
            agent.isStopped = true;
        }

        

    }


   

}
