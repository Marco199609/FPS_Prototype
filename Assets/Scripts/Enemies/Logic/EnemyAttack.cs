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
    EnemyMove enemyMove;
    //For use in case of ray detection
    LineRenderer lineRenderer;
    Material lineMaterial;
    Transform spawnPoint;


    void SetVariables()
    {
        if(player == null)
            player = GameObject.FindWithTag("Player");
        if(attackSound == null)
            attackSound = enemyData.attackSound;
        if (enemyMove == null)
            enemyMove = gameObject.GetComponent<EnemyMove>();
    }

    private void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        lineRenderer = enemyData.lineRenderer;
        lineMaterial = lineRenderer.material;
        spawnPoint = enemyData.spawnPoint;
    }


    private void FixedUpdate()
    {
        SetVariables();

        if(playerDetected)
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (enemyData.rayPlayerDetection)
        {
            DetectionWithRays(lineRenderer, spawnPoint);
            AttackWithRays();
        }
        else
        {
            AttackWithBulletsOrLights();
        }
    }

    void AttackWithRays()
    {
        if(playerDetected)
        {
            lineMaterial.SetColor("_EmissionColor", Color.red);


            if (!attackSound.isPlaying)
                attackSound.Play();

            if (muzzleFlash != null)
                muzzleFlash.SetActive(true);

            player.GetComponent<PlayerController>().playerHealth -= Time.deltaTime * 8;
        }
        else
        {
            lineMaterial.SetColor("_EmissionColor", Color.green);

            if (attackSound.isPlaying)
                attackSound.Stop();

            if (muzzleFlash != null)
                muzzleFlash.SetActive(false);
        }
    }

    void AttackWithBulletsOrLights()
    {
        if(playerDetected)
        {
            if (!attackSound.isPlaying)
                attackSound.Play();
            enemyData.enemyLight.GetComponent<Light>().color = Color.red;
            enemyData.enemyLight.GetComponent<VLight>().colorTint = Color.red;
            //enemyMove.enabled = false;
        }
        else
        {
            if (attackSound.isPlaying)
                attackSound.Stop();
            enemyData.enemyLight.GetComponent<Light>().color = Color.white;
            enemyData.enemyLight.GetComponent<VLight>().colorTint = Color.white;
            //enemyMove.enabled = true;
        }
    }
}
                                                                                                                                                                                                                                                                          