using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyController enemyController;
    EnemyData enemyData;
    public int health;

    private void Start()
    {
        
    }
    void SetVariables()
    {
        if (enemyController == null)
            enemyController = gameObject.GetComponent<EnemyController>();
        if(enemyData == null)
        {
            enemyData = gameObject.GetComponent<EnemyData>();
            health = enemyData.health;
        }
    }

    void FixedUpdate()
    {
        SetVariables();
        Death();
    }


    void Death()
    {
        if (health <= 0)
        {
            var explosion = Instantiate(enemyData.explosionPrefab, transform.position, transform.rotation);
            var burnedEnemy = Instantiate(enemyData.burnedEnemy, transform.position, transform.rotation);

            if(enemyData.isAIObject)
            {
                burnedEnemy.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
                Destroy(burnedEnemy.GetComponent<Rigidbody>(), 5f);
                Destroy(burnedEnemy.GetComponent<Collider>(), 6f);
            }

            burnedEnemy.transform.SetParent(gameObject.transform.parent);
            Destroy(explosion, 5f);
            Destroy(gameObject);
        }
    }
}
