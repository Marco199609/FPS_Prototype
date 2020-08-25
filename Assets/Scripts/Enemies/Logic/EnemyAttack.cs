using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Transform laserSpawn;

    public float sphereRadius;
    public float distance;

    public GameObject enemyLight;
    public bool attacking;

    void FixedUpdate()
    {
        //SetVariables();
        //LaserControl();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyLight.GetComponent<Light>().color = Color.red;
            enemyLight.GetComponent<VLight>().colorTint = enemyLight.GetComponent<Light>().color;
            attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyLight.GetComponent<Light>().color = Color.white;
            enemyLight.GetComponent<VLight>().colorTint = enemyLight.GetComponent<Light>().color;
            attacking = false;
        }
    }


}
