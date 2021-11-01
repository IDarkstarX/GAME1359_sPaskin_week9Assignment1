using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public int startingHealth = 100;

    [SyncVar]
    public int currentHealth;

    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Dictionary<GameObject, bool> PML;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponent <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    void Start()
    {
        PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
    }

    void Update ()
    {
        
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        
        if (PML != null)
        {
            foreach (var i in PML)
            {
                if(i.Key.GetComponent<PlayerHealth>() == this && currentHealth <= 0 && i.Value)
                {
                    Debug.Log("Setting a player as dead!");
                    PML[i.Key] = false;
                    GameObject.Find("HUDCanvasALL").GetComponent<GameOverManager>().deadPlayers++;
                    return;
                } else
                {
                    Debug.Log("No one new has died.");
                }
            }
        }
        else
        {
            PML = GameObject.FindGameObjectWithTag("Player Master List").GetComponent<PlayerMasterList>().playerList;
        }
        
    }


    public void TakeDamage (int amount)
    {
        //if (!isServer) return;

        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
