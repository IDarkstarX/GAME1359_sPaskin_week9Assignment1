using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.SceneManagement;

public class GameOverManager : NetworkBehaviour
{
    PlayerHealth playerHealth;
	public float restartDelay = 5f;

    Dictionary<GameObject, bool> PML;

    Animator anim;
	float restartTimer;

    public int deadPlayers = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }


    void Update()
    {
        Debug.Log("Dead players: "+deadPlayers+ " - Total players: " + PML.Count);
        

        if (deadPlayers >= PML.Count)
        {
            restartTimer += Time.deltaTime;
            Debug.Log("ENDING GAME!");
            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
