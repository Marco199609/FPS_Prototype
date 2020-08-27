using UnityEngine;

public class EnemyMove : EnemyAI
{
    EnemyData enemyData;
    EnemyPlayerDetection enemyPlayerDetection;
    GameObject[] pathToFollow;
    GameObject player;
    bool findDistanceToPlayer = true;
    Vector3 playerVelocityVector;
    float playerVelocity;


    void SetVariables()
    {
        if (enemyData == null)
            enemyData = GetComponent<EnemyData>();
        if (enemyPlayerDetection == null)
            enemyPlayerDetection = gameObject.GetComponent<EnemyPlayerDetection>();
        if (pathToFollow == null)
        {
            pathToFollow = GameObject.FindGameObjectsWithTag(enemyData.pathObjectsTag);
            enemyData.pathToFollow = pathToFollow;
        }
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }



    private void FixedUpdate()
    {
        SetVariables();

        if(!enemyPlayerDetection.playerDetected)
        {
            FollowPath(pathToFollow, enemyData.speed, enemyData.rotateSpeed, enemyData.minDirTimer, enemyData.maxDirTimer);
            findDistanceToPlayer = true;
        }
        else
        {
            if(findDistanceToPlayer)                                                                                        //
            {                                                                                                               //
                playerVelocityVector = player.transform.position;                                                           //
                findDistanceToPlayer = false;                                                                               //     Gets player velocity
            }                                                                                                               //
            playerVelocity = ((player.transform.position - playerVelocityVector).magnitude) / Time.deltaTime;               //
            playerVelocityVector = player.transform.position;                                                               //

            transform.position += transform.forward * playerVelocity * Time.deltaTime;                                      //Moves forward at player speed
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyData.pathObjectsTag)
        {
            ChangePathTimer = 0;
        }
    }
}
