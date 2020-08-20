using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int totalAmmo, currentAmmo;
    public Animator shootAnimator, gunSightAnimator;
    public Transform spawnPoint;
    public GameObject muzzleFlash;
    public bool isAutomatic;
    public float automaticFireRate = 0.07f;
    public AudioSource shootSound, aimSound, clickSound;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = totalAmmo;
    }

    void Update()
    {
        
    }
}
