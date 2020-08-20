using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2 : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    Animator shootAnimator, gunSightAnimator;
    Transform spawnPoint;
    GameObject muzzleFlash;
    bool isAutomatic, reloading, shooting, sighting;
    int totalAmmo, currentAmmo;
    float automaticFireRate;
    AudioSource shootSound, aimSound, clickSound;

    void SetVariables()
    {
        isAutomatic = currentWeapon.isAutomatic;
        currentAmmo = currentWeapon.currentAmmo;

        if (totalAmmo == 0)
            totalAmmo = currentWeapon.totalAmmo;
        if (automaticFireRate <= 0)
            automaticFireRate = currentWeapon.automaticFireRate;
        if (shootAnimator == null)
            shootAnimator = currentWeapon.shootAnimator;
        if (gunSightAnimator == null)
            gunSightAnimator = currentWeapon.gunSightAnimator;
        if(spawnPoint == null)
            spawnPoint = currentWeapon.spawnPoint;
        if(muzzleFlash == null)
            muzzleFlash = currentWeapon.muzzleFlash;
        if (shootSound == null)
            shootSound = currentWeapon.shootSound;
        if (aimSound == null)
            aimSound = currentWeapon.aimSound;
        if (clickSound == null)
            clickSound = currentWeapon.clickSound;
    }

    private void Update()
    {
        InputControl();
    }
    void FixedUpdate()
    {
        SetVariables();
        ShootWeapon();
        ReloadWeapon();
        GunSight();
    }


    void ShootWeapon()
    {
        if(shooting)
        {
            if (isAutomatic)
            {
                shootAnimator.speed = 1 / (automaticFireRate * 10);

                automaticFireRate -= Time.fixedDeltaTime;

                if(automaticFireRate <= 0)
                {
                    if (currentAmmo > 0)
                    {
                        shootAnimator.SetBool("Shooting", true);
                        muzzleFlash.SetActive(true);
                        currentWeapon.currentAmmo--;
                        shootSound.Play();
                    }
                    else
                    {
                        shootAnimator.SetBool("Shooting", false);
                        muzzleFlash.SetActive(false);
                        print("Reload!");
                    }
                    automaticFireRate = currentWeapon.automaticFireRate;
                }
                else
                {
                    muzzleFlash.SetActive(false);
                }
            }
            else
            {
                shootAnimator.speed = 1;

                if (currentAmmo > 0)
                {
                    shootAnimator.SetBool("Shooting", true);
                    muzzleFlash.SetActive(true);
                    currentWeapon.currentAmmo--;
                    shootSound.Play();
                }
                else
                    print("Reload!");

                shooting = false;
            }
        }
        else
        {
            shootAnimator.SetBool("Shooting", false);
            muzzleFlash.SetActive(false);
        }
    }

    void GunSight()
    {
        if (sighting)
        {
            gunSightAnimator.SetBool("GunSight", true);
            aimSound.pitch = 1;
        }
        else
        {
            gunSightAnimator.SetBool("GunSight", false);
            aimSound.pitch = 0.8f;
        }
            
    }

    void ReloadWeapon()
    {
        if(reloading)
        {
            if (currentAmmo < totalAmmo)
                currentWeapon.currentAmmo++;
            else
                reloading = false;
        }
    }

    void InputControl()
    {
        if (Input.GetMouseButtonDown(0))
            shooting = true;

        if (Input.GetMouseButtonUp(0))
            if (isAutomatic)
                shooting = false;

        if (Input.GetKeyDown(KeyCode.R))
            reloading = true;

        if (Input.GetMouseButtonDown(1))
        {
            sighting = true;
            aimSound.Play();
        }
        if(Input.GetMouseButtonUp(1))
        {
            sighting = false;
            aimSound.Play();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            clickSound.Play();
            if (isAutomatic)
                currentWeapon.isAutomatic = false;
            else
                currentWeapon.isAutomatic = true;
        }
    }
}
