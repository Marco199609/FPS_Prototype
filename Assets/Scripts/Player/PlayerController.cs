using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject torch;
    [SerializeField] GameObject[] bloodSprites;
    [SerializeField] float bloodTransparencyIndex = 1;
    [SerializeField] AudioSource heartBeat;
    [SerializeField] Text healthText;
    bool transparentBlood;
    public float playerHealth = 18;


    // Update is called once per frame
    void FixedUpdate()
    {

        HealthControl();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (torch.activeInHierarchy)
            {
                torch.SetActive(false);
            }
            else
            {
                torch.SetActive(true);
            }
        }
    }

    void HealthControl()
    {
        if(playerHealth <= 18)
        playerHealth += Time.deltaTime * 0.3f;


        if (playerHealth <= 15)
        {
            if (!heartBeat.isPlaying)
                heartBeat.Play();

            if (playerHealth >= 9)
                heartBeat.volume = 0.5f;
            else
                heartBeat.volume = 1f;
        }
        else
            if (heartBeat.isPlaying)
                heartBeat.Stop();



        if (playerHealth <= 15 && playerHealth > 12)
            bloodSprites[0].SetActive(true);
        else if (playerHealth <= 12 && playerHealth > 9)
            bloodSprites[1].SetActive(true);
        else if (playerHealth <= 9 && playerHealth > 6)
            bloodSprites[2].SetActive(true);
        else if (playerHealth <= 6 && playerHealth > 4)
            bloodSprites[3].SetActive(true);
        else if (playerHealth <= 4 && playerHealth > 1)
            bloodSprites[4].SetActive(true);
        else if (playerHealth <= 0)
            bloodSprites[5].SetActive(true);
        if(playerHealth > 15)
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




        healthText.text = "Health: " + (playerHealth * 5.555555555555556f).ToString("0");
    }
}
