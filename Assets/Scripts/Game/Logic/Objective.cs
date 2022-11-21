using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    GameController gameController;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (gameController.getObjective)
            //{
              //  gameController.getObjective = false;
               // gameController.objectiveCompleted = true;
                gameController.openElevatorDoors = true;
                Destroy(gameObject);
            //}
        }
        else
            gameController.getObjective = false;
    }
}
