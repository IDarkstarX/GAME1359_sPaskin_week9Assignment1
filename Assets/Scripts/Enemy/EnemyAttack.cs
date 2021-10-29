﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;

    //GameObject player;
    PlayerHealth playerHealth;

    Dictionary<GameObject, bool> PML;

    Transform currentTarget;

    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag ("Player");
        //playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        PML = GameObject.Find("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == currentTarget)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == currentTarget)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        float shortestDist = float.MaxValue;
        if (PML != null)
        {
            foreach (var i in PML)
            {
                float dist = Vector3.Distance(this.transform.position, i.Key.transform.position);

                if (dist < shortestDist)
                {
                    currentTarget = i.Key.transform;
                    playerHealth = currentTarget.GetComponent<PlayerHealth>();
                    shortestDist = dist;
                }
            }
        }

        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}