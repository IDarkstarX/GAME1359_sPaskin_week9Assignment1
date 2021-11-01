using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class EnemyManager : NetworkBehaviour
{
    //PlayerHealth playerHealth;

    Dictionary<GameObject, bool> PML;

    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
        PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }

    void Spawn ()
    {
        if (!isServer) return;

        if (PML != null)
        {
            foreach (var i in PML)
            {
                if (i.Key.GetComponent<PlayerHealth>().currentHealth <= 0f || !i.Value)
                {
                    return;
                }
            }
        }
        else
        {
            PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
        }
        
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        GameObject g = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        NetworkServer.Spawn(g);
    }
}
