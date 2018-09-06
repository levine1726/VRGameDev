using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMonsterHealthDamage : MonoBehaviour {

    public float health = 10f;
    public float damage = 10f;
    public float damageTakenFromBullet = 10f;
    [HideInInspector]
    public bool readyToAttack;
    private Animator anim;
    [HideInInspector]
    public TreeHealth treeToAttack;
    [HideInInspector]
    public bool destructionSignaled = false;
    
    private string[] ANIMATION_ATTACK_STATES = { "Attack1", "Attack2", "Attack3" };
   

    private string ANIMATION_WALKING = "Walking";

    private AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip dieSound;
    

    private bool animationAttackStateBeenSet = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (treeToAttack != null)
        {
            
            if (!treeToAttack.GetIsPlayerOnTree())
            {
                treeToAttack = null;
            }
        } else
        {
            EnableWalkingAndDisableAttacking();
        }

        if (readyToAttack && (treeToAttack != null))
        {
            AttackTree();

        }

        if (health <= 0 && !destructionSignaled)
        {
            DestroyMonster(false);
        }

        if (EndGameDetection.endgameTriggered)
        {
            DestroyMonster(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Tree")
        {
            treeToAttack = collision.GetComponent<TreeHealth>();
            if (treeToAttack.GetIsPlayerOnTree())
            {
                //TODO: disable monster movement and switch animation to attack

                readyToAttack = true;
            }
        }

        if (collision.tag == "Bullet")
        {
            if (health > 0)
            {
                TakeDamage();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Tree")
        {
            treeToAttack = null;
            readyToAttack = false;
            
        }
    }

    private void AttackTree()
    {
        if (!animationAttackStateBeenSet)
        {
            SetAttackState();
        }
        treeToAttack.TakeDamage(damage);
        readyToAttack = false;
        StartCoroutine(DisableAttack());

    }


    private void SetAttackState()
    {
        int attackType =  Random.Range(0,  ANIMATION_ATTACK_STATES.Length - 1);
        anim.SetBool(ANIMATION_ATTACK_STATES[attackType], true);
        anim.SetBool(ANIMATION_WALKING, false);
        animationAttackStateBeenSet = true;
    }

    IEnumerator DisableAttack()
    {
        yield return new WaitForSecondsRealtime(3f);
        readyToAttack = true;
    }

    private void TakeDamage()
    {
        health -= damageTakenFromBullet;
        anim.Play("creature1GetHit");
        int chanceToPlayShot = Random.Range(0, 1);
        if (chanceToPlayShot == 0)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void EnableWalkingAndDisableAttacking()
    {
        anim.SetBool(ANIMATION_ATTACK_STATES[0], false);
        anim.SetBool(ANIMATION_ATTACK_STATES[1], false);
        anim.SetBool(ANIMATION_ATTACK_STATES[2], false);
        anim.SetBool(ANIMATION_WALKING, true);
        animationAttackStateBeenSet = false;
    }

    public void DestroyMonster(bool gameEnded)
    {
        
        if (!gameEnded)
        {
            destructionSignaled = true;
            anim.Play("creature1Die 0");
            audioSource.PlayOneShot(dieSound);
            TerrainGenerator.monstersOnField--;
            TerrainGenerator.monstersKilled++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    private void MonsterDied()
    {
        Destroy(gameObject);
    }

    

}

// 437 - 464