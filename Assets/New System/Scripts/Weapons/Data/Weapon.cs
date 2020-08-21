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
    public float automaticFireRate = 0.07f, reloadTime = 1.3f, clickSoundTime, magSoundTime, reloadSoundTime;
    public AudioSource shootSound, aimSound, clickSound, magSlideSound, reloadSound;
    [SerializeField] Vector3 initialOffset;
    void Start()
    {
        transform.localPosition = initialOffset;
        currentAmmo = totalAmmo;
    }
}
