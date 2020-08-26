using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
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
                        

}
                                                                                                                                                                                                                                                                          