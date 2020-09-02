using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputControl();
    }



    void InputControl()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            gameController.getObjective = true;
        }
    }
}
