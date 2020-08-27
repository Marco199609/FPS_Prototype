using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyPlayerDetection
{
    //For global use
    EnemyData enemyData;
    GameObject player;
    AudioSource attackSound;
    GameObject muzzleFlash;
    //For use in case of ray detection
    LineRenderer lineRenderer;
    Material lineMaterial;
    Transform spawnPoint;



    private void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        lineRenderer = enemyData.lineRenderer;
        lineMaterial = lineRenderer.material;
        spawnPoint = enemyData.spawnPoint;
        player = GameObject.FindWithTag("Player");
        attackSound = enemyData.attackSound;
    }


    private void FixedUpdate()
    {
        if (enemyData.rayPlayerDetection)
        {
            DetectionWithRays(lineRenderer, spawnPoint);
            AttackWithRays();
        }
    }


    void AttackWithRays()
    {
        if(playerDetected)
        {
            lineMaterial.SetColor("_EmissionColor", Color.red);

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z));

            if (!attackSound.isPlaying)
                attackSound.Play();

            if (muzzleFlash != null)
                muzzleFlash.SetActive(true);

            player.GetComponent<PlayerController>().playerHealth -= Time.deltaTime * 8;
        }
    }



    /*
    public float sphereRadius;
    public float distance;

    public GameObject enemyLight;
    public bool attacking;

    void FixedUpdate()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyLight.GetComponent<Light>().color = Color.red;
            attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyLight.GetComponent<Light>().color = Color.white;                                                                                                      
            attacking = false;
        }                       
    }
          */

}
                                                                                                                                                                                                                                                                          