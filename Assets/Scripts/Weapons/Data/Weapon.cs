using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammoCapacity, currentAmmo, availableAmmo, damage;
    public Animator meshAnimator, parentAnimator;
    public Transform aimPoint;
    public GameObject muzzleFlash;
    public bool autoModeOn, isAutoWeapon;
    public float fireRate, reloadTime = 1.3f;
    public AudioSource shootSound, aimSound, clickSound, magSlideSound, reloadSound;
    public Vector3 initialOffset, initialRotation;
    void Start()
    {
        transform.localPosition = initialOffset;
        currentAmmo = ammoCapacity;
    }
}
