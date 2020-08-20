using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int totalAmmo, currentAmmo;
    public Animator meshAnimator, parentAnimator;
    public Transform spawnPoint;
    public GameObject muzzleFlash;
    public bool isAutomatic;
    public float automaticFireRate = 0.07f, reloadTime = 1.3f;
    public AudioSource shootSound, aimSound, clickSound;
    [SerializeField] Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = initialOffset;
        currentAmmo = totalAmmo;
    }

    void Update()
    {
        
    }
}
