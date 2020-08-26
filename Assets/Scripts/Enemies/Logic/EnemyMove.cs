using UnityEngine;

public class EnemyMove : EnemyAI
{
    EnemyData enemyData;
    GameObject[] pathToFollow;



    void SetVariables()
    {
        if (enemyData == null)
            enemyData = GetComponent<EnemyData>();
        if (pathToFollow == null)
        {
            pathToFollow = GameObject.FindGameObjectsWithTag("Path");
            enemyData.pathToFollow = pathToFollow;
        }
    }



    private void FixedUpdate()
    {
        SetVariables();
        FollowPath(pathToFollow, enemyData.speed, enemyData.rotateSpeed, enemyData.minDirTimer, enemyData.maxDirTimer);
    }
}
