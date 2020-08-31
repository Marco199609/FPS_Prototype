using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] bool playerInElevator, openDoors;
    public bool[] levelActive;
    [SerializeField] GameObject door1, door2;
    [SerializeField] GameObject[] levelTriggers;
    // Start is called before the first frame update
    void Start()
    {
        levelActive = new bool[levelTriggers.Length];
    }

    // Update is called once per frame
    void Update()
    {
        DoorOpenOrClose();
    }


    void DoorOpenOrClose()
    {
        if(openDoors)
        {
            door1.transform.localPosition = Vector3.Lerp(door1.transform.localPosition, new Vector3(door1.transform.localPosition.x, door1.transform.localPosition.y, -2.53f), 0.01f);
            door2.transform.localPosition = Vector3.Lerp(door2.transform.localPosition, new Vector3(door2.transform.localPosition.x, door2.transform.localPosition.y, 5.58f), 0.01f);
        }
        else
        {
            door1.transform.localPosition = Vector3.Lerp(door1.transform.localPosition, new Vector3(door1.transform.localPosition.x, door1.transform.localPosition.y, 0.2487488f), 0.01f);
            door2.transform.localPosition = Vector3.Lerp(door2.transform.localPosition, new Vector3(door2.transform.localPosition.x, door2.transform.localPosition.y, 2.751251f), 0.01f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInElevator = true;
        }

        for (int i = 0; i < levelTriggers.Length; i++)
        {
            if (other == levelTriggers[i].GetComponent<Collider>())
                levelActive[i] = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInElevator = false;
        }


        for (int i = 0; i < levelTriggers.Length; i++)
        {
            if (other == levelTriggers[i].GetComponent<Collider>())
                levelActive[i] = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
