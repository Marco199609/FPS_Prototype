using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    PlayerController playerController;
    float playerHealth, bloodTransparencyIndex = 1;
    bool transparentBlood;
    GameObject[] bloodSprites;

    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();
        if (bloodSprites.Length == 0)
        {
            bloodSprites = new GameObject[playerController.bloodSprites.Length];
            bloodSprites = playerController.bloodSprites;
        }

        playerHealth = playerController.playerHealth;
    }

    private void Start()
    {
        bloodSprites = new GameObject[0];
    }


    void FixedUpdate()
    {
        SetVariables();
        HealthHUD();
    }

    void HealthHUD()
    {
        if (playerHealth <= 50 && playerHealth > 0)
            bloodSprites[(int)playerHealth / 8].SetActive(true);            //Divided by 8 to match the 6 bloodsprites
        if (playerHealth <= 0)
            bloodSprites[0].SetActive(true);


        if (playerHealth > 50)
        {
            for (int i = 0; i < bloodSprites.Length; i++)
            {
                bloodSprites[i].SetActive(false);
            }
        }

        if (transparentBlood)
            bloodTransparencyIndex -= Time.deltaTime * 1.05f;
        else
            bloodTransparencyIndex += Time.deltaTime * 1.05f;

        if (bloodTransparencyIndex <= 0.1f)
            transparentBlood = false;
        if (bloodTransparencyIndex >= 0.6f)
            transparentBlood = true;

        for (int i = 0; i < bloodSprites.Length; i++)
        {
            bloodSprites[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, bloodTransparencyIndex);
        }
        playerController.healthText.text = playerHealth.ToString("0");
    }
}
