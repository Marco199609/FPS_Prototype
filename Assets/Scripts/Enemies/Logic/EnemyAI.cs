using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    int pathPointNumber;                //Sets the index of the target to be followed
    bool once;
    public float ChangePathTimer;              //Time for direction change
    Vector3 direction;                  //rotation direction
    Ray pathRay;                    
    RaycastHit hit;

    public void FollowPath(GameObject[] pathToFollow, float speed = 5, float rotateSpeed = 2, float minTimer = 5, float maxTimer = 10)
    {
        if(!once)
        {
            pathPointNumber = Random.Range(0, pathToFollow.Length - 1);
            transform.position = pathToFollow[pathPointNumber].transform.position;
            ChangePathTimer = Random.Range(2, 5);
            once = true;
        }

        ChangePathTimer -= Time.deltaTime;



        if (ChangePathTimer <= 0)
        {
            pathPointNumber = pathPointNumber = Random.Range(0, pathToFollow.Length - 1);                                                   //Changes target if counter reaches 0
            ChangePathTimer = Random.Range(minTimer, maxTimer);
        }

        pathRay = new Ray(transform.position, pathToFollow[pathPointNumber].transform.position - transform.position);                       //Ray points to new target

        if (Physics.Raycast(pathRay.origin, pathRay.direction, out hit, 500))
        {
            if(hit.collider != pathToFollow[pathPointNumber].GetComponent<Collider>())
            {
                pathPointNumber = pathPointNumber = Random.Range(0, pathToFollow.Length - 1);                                               //if there is an obstacle, finds another target
                Debug.DrawRay(pathRay.origin, pathRay.direction * hit.distance, Color.red);
            }
            else                                                                                                                            //If there is no obstacle, rotate and move towards target
            {
                gameObject.transform.position += transform.forward * speed * Time.deltaTime;                                                //Forward movement

                direction = pathToFollow[pathPointNumber].transform.position - transform.position;                                          //
                direction = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);                        //Rotation Movement
                transform.rotation = Quaternion.LookRotation(direction);                                                                    //
                Debug.DrawRay(pathRay.origin, pathRay.direction * hit.distance, Color.green);
            }

        }
        else if(transform.position == pathToFollow[pathPointNumber].transform.position)
        {
            pathPointNumber = pathPointNumber = Random.Range(0, pathToFollow.Length - 1);                                           //if reaches target, find another target
            Debug.DrawRay(pathRay.origin, pathRay.direction * hit.distance, Color.red);
        }
            
    }
}
