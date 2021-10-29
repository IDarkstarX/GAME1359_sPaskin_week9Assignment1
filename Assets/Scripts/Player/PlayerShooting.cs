using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    [SerializeField]
    GameObject gun;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = gun.GetComponent<ParticleSystem> ();
        gunLine = gun.GetComponent <LineRenderer> ();
        gunAudio = gun.GetComponent<AudioSource> ();
        gunLight = gun.GetComponent<Light> ();
    }


    void Update ()
    {
        if (!isLocalPlayer) return;

        timer += Time.deltaTime;
        
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            CmdShoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            CmdDisableEffects ();
        }
    }

    [Command]
    public void CmdDisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    [Command]
    void CmdShoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, gun.transform.position);

        shootRay.origin = gun.transform.position;
        shootRay.direction = gun.transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
