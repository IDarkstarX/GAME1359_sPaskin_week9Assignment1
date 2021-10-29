using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class GameOverManager : NetworkBehaviour
{
    PlayerHealth playerHealth;
	public float restartDelay = 5f;

    Dictionary<GameObject, bool> PML;

    Animator anim;
	float restartTimer;


    void Start()
    {
        anim = GetComponent<Animator>();
        PML = GameObject.Find("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				Application.LoadLevel(Application.loadedLevel);
			}
        }
    }
}
