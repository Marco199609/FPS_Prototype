using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyData enemyData;
    EnemyMove enemyMove;
    EnemyHealth enemyHealth;


    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        if (enemyData.isAIObject)
            enemyMove = gameObject.AddComponent<EnemyMove>();
        enemyHealth = gameObject.AddComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
