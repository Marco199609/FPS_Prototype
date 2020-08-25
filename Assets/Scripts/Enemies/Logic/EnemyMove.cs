using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    EnemyAttack enemyAttack;
    int randomTurnAngle;
    [SerializeField] float turnTimer = 5;
    GameObject player;
    RaycastHit hit;
    // Start is called before the first frame update


    void Start()
    {
        transform.position = new Vector3(Random.Range(-230, 230), 15, Random.Range(-230, 230));
        turnTimer = Random.Range(1, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyAttack == null)
            enemyAttack = gameObject.GetComponent<EnemyAttack>();
        if (player == null)
            player = GameObject.FindWithTag("Player");

        if(enemyAttack.attacking == false)
        {
            RandomMoveDir();
        }
        else
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            transform.position += transform.forward * Time.deltaTime * 10;
        }

    }


    void RandomMoveDir()
    {
        transform.position = new Vector3(transform.position.x, 15, transform.position.z);


        turnTimer -= Time.deltaTime;

        if (turnTimer <= 0)
        {
            randomTurnAngle = Random.Range(-180, 180);
            turnTimer = Random.Range(2, 5);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + randomTurnAngle, 0), 0.05f);

        transform.position += transform.forward * Time.deltaTime * 10;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            randomTurnAngle = 180;
            turnTimer = Random.Range(2, 5);
        }

        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
    }
}
