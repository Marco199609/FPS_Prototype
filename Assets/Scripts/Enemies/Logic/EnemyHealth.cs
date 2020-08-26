using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
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
            burnedEnemy.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
            Destroy(burnedEnemy, 10f);
            Destroy(explosion, 5f);
            Destroy(gameObject);
        }
    }
}
