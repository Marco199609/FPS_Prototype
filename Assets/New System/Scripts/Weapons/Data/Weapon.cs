using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammoCapacity, currentAmmo, availableAmmo;
    public Animator meshAnimator, parentAnimator;
    public Transform spawnPoint;
    public GameObject muzzleFlash;
    public bool isAutomatic;
    public float automaticFireRate = 0.07f, reloadTime = 1.3f, clickSoundTime, magSoundTime, reloadSoundTime, damage;
    public AudioSource shootSound, aimSound, clickSound, magSlideSound, reloadSound;
    public Vector3 initialOffset, initialRotation;
    void Start()
    {
        transform.localPosition = initialOffset;
        currentAmmo = ammoCapacity;
    }
}
