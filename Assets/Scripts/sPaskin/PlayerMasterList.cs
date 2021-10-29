using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMasterList : NetworkBehaviour
{

    public Dictionary<GameObject, bool> playerList = new Dictionary<GameObject, bool>();

    void Start()
    {

    }
    
    void Update()
    {
        foreach (var i in playerList)
        {
            Debug.Log(i.Key + ": " + i.Value);
        }
    }

    void OnPlayerConnected(NetworkIdentity player)
    {
        Debug.Log("Player " + " connected from " + player.netId);
        playerList.Add(player.gameObject, true);
    }
}

/*
   float shortestDist = float.MaxValue;
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

    [SerializeField]
    Dictionary<GameObject, bool> PML = GameObject.Find("Player Master List").GetComponent<PlayerMasterList>().playerList;
*/