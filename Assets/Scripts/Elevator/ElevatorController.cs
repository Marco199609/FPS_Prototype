using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    GameController gameController;
    bool openDoors;
    GameObject door1, door2;
    public float elevatorTimer;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        elevatorTimer = gameController.elevatorTimer;
        gameController.levelActive = new bool[gameController.levelTriggers.Length];
    }


    void SetVariables()
    {
        if (gameController == null)
            gameController = gameObject.GetComponent<GameController>();

        if (door1 == null)
            door1 = gameController.elevatorDoor1;
        if (door2 == null)
            door2 = gameController.elevatorDoor2;

        openDoors = gameController.openElevatorDoors;

    }


    void FixedUpdate()
    {
        SetVariables();
        DoorOpenOrClose();
        GoToNextLevel();
    }


    void DoorOpenOrClose()
    {
        if (openDoors)
        {
            door1.transform.localPosition = Vector3.Lerp(door1.transform.localPosition, new Vector3(door1.transform.localPosition.x, door1.transform.localPosition.y, -2.7f), 0.03f);
            door2.transform.localPosition = Vector3.Lerp(door2.transform.localPosition, new Vector3(door2.transform.localPosition.x, door2.transform.localPosition.y, 5.63f), 0.03f);
        }
        else
        {
            door1.transform.localPosition = Vector3.Lerp(door1.transform.localPosition, new Vector3(door1.transform.localPosition.x, door1.transform.localPosition.y, 0.114f), 0.03f);
            door2.transform.localPosition = Vector3.Lerp(door2.transform.localPosition, new Vector3(door2.transform.localPosition.x, door2.transform.localPosition.y, 2.904f), 0.03f);
        }

    }


    void GoToNextLevel()
    {
        if(!openDoors && gameController.playerInElevator)
        {
            elevatorTimer -= Time.deltaTime;
            if(gameController.objectives[1] != null)
            {
                if(elevatorTimer <= gameController.elevatorTimer - 4)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 19.13f, transform.position.z), 0.02f);
            }
            else
            {
                if(elevatorTimer <= gameController.elevatorTimer - 4)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 29.3f, transform.position.z), 0.02f);
            }


            if(elevatorTimer <= 0)
            {
                gameController.openElevatorDoors = true;
                gameController.doorSound.Play();
                elevatorTimer = gameController.elevatorTimer;
                gameController.objectiveCompleted = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < gameController.levelTriggers.Length; i++)
        {
            if (other == gameController.levelTriggers[i].GetComponent<Collider>())
                gameController.levelActive[i] = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (openDoors)
                gameController.doorSound.Play();
            gameController.playerInElevator = true;
            gameController.openElevatorDoors = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameController.playerInElevator = false;
            gameController.openElevatorDoors = false;
        }


        for (int i = 0; i < gameController.levelTriggers.Length; i++)
        {
            if (other == gameController.levelTriggers[i].GetComponent<Collider>())
                gameController.levelActive[i] = false;
        }
    }
}
