using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("To use for FPS count")]
    public int targetFramerate = 0;
    public Text _fpsText;


    [Header("Level control")]
    public GameObject[] levels, levelTriggers;
    public bool[] levelActive;

    [Header("Objectives Control")]
    public GameObject[] objectives;
    public bool objectiveCompleted;
    public bool getObjective;

    [Header("Elevator control")]
    [SerializeField] GameObject elevator;
    public bool playerInElevator, openElevatorDoors;
    public GameObject elevatorDoor1, elevatorDoor2;
    public float elevatorTimer;
    public AudioSource doorSound;



    private void Start()
    {
        gameObject.AddComponent<FPSControl>();
        gameObject.AddComponent<InputController>();
        gameObject.AddComponent<LevelController>();
        elevator.AddComponent<ElevatorController>();
        for(int i = 0; i < objectives.Length; i++)
            objectives[i].AddComponent<Objective>();
    }
}
