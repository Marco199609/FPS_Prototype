using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController;
    float playerHealth;
    AudioSource heartBeat;


    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();

        playerHealth = playerController.playerHealth;

        if (heartBeat == null)
            heartBeat = playerController.heartBeat;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        SetVariables();
        HealthControl();
    }



    void HealthControl()
    {
        if (playerHealth <= 100)
            playerController.playerHealth += Time.deltaTime * 1f;

        if (playerHealth <= 50)
        {
            if (!heartBeat.isPlaying)
                heartBeat.Play();

            if (playerHealth >= 20)
                heartBeat.volume = 0.5f;
            else
                heartBeat.volume = 1f;
        }
        else
            if (heartBeat.isPlaying)
            heartBeat.Stop();
    }
}
