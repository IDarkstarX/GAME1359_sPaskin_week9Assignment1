using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class EnemyManager : NetworkBehaviour
{
    PlayerHealth playerHealth;

    Dictionary<GameObject, bool> PML;

    Transform currentTarget;

    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
        PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }

    private void Update()
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
        } else
        {
            PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
        }
    }

    void Spawn ()
    {

        if (!isServer) return;

        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        NetworkServer.Spawn(enemy);
    }
}
