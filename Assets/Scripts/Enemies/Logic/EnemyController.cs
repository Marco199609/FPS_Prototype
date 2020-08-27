using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyData enemyData;
    EnemyMove enemyMove;
    EnemyHealth enemyHealth;
    EnemyPlayerDetection enemyPlayerDetection;
    EnemyAttack enemyAttack;
    EnemyRotate enemyRotate;
    IsVisible isVisible;


    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        if (enemyData.isAIObject)                                       //Adds movement AI if movable enemy
            enemyMove = gameObject.AddComponent<EnemyMove>();
        enemyHealth = gameObject.AddComponent<EnemyHealth>();
        enemyPlayerDetection = gameObject.AddComponent<EnemyPlayerDetection>();
        enemyAttack = gameObject.AddComponent<EnemyAttack>();
        if(enemyData.isStaticRotateObj)
            enemyRotate = gameObject.AddComponent<EnemyRotate>();
        isVisible = gameObject.AddComponent<IsVisible>();

    }
}
