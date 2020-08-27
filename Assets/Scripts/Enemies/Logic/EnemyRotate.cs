using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    EnemyData enemyData;
    EnemyPlayerDetection enemyPlayerDetection;
    Vector3 rotationAxis;
    // Start is called before the first frame update
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemyPlayerDetection = gameObject.GetComponent<EnemyPlayerDetection>();
        rotationAxis = enemyData.rotationAxis;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!enemyPlayerDetection.playerDetected)
            transform.Rotate(rotationAxis);
    }
}
