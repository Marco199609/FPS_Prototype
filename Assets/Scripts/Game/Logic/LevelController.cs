using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LevelControl();
    }

    void LevelControl()
    {
        for (int i = 0; i < gameController.levels.Length; i++)
        {
            if (gameController.levelActive[i] == true)
                gameController.levels[i].SetActive(true);
            else
                gameController.levels[i].SetActive(false);
        }
    }
}
