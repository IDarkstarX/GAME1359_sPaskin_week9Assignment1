using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class EnemyMovement : NetworkBehaviour
{
    //Transform player;
    PlayerHealth playerHealth;

    [SyncVar]
    Transform currentTarget;

    Dictionary<GameObject, bool> PML;

    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag ("Player").transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
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
        else
        {
            PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
        }

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (currentTarget.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}


