using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2 : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    Animator meshAnimator, parentAnimator;
    Transform spawnPoint;
    GameObject muzzleFlash;
    bool isAutomatic, reloading, shooting, sighting;
    int totalAmmo, currentAmmo;
    float automaticFireRate, reloadTime;
    AudioSource shootSound, aimSound, clickSound;

    void SetVariables()
    {
        isAutomatic = currentWeapon.isAutomatic;
        currentAmmo = currentWeapon.currentAmmo;

        if (reloadTime == 0)
            reloadTime = currentWeapon.reloadTime;
        if (totalAmmo == 0)
            totalAmmo = currentWeapon.totalAmmo;
        if (automaticFireRate <= 0)
            automaticFireRate = currentWeapon.automaticFireRate;
        if (meshAnimator == null)
            meshAnimator = currentWeapon.meshAnimator;
        if (parentAnimator == null)
            parentAnimator = currentWeapon.parentAnimator;
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

        if(!reloading)
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
                meshAnimator.speed = 1 / (automaticFireRate * 10);

                automaticFireRate -= Time.fixedDeltaTime;

                if(automaticFireRate <= 0)
                {
                    if (currentAmmo > 0)
                    {
                        meshAnimator.SetBool("Shooting", true);
                        muzzleFlash.SetActive(true);
                        currentWeapon.currentAmmo--;
                        shootSound.Play();
                    }
                    else
                    {
                        meshAnimator.SetBool("Shooting", false);
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
                meshAnimator.speed = 1;

                if (currentAmmo > 0)
                {
                    meshAnimator.SetBool("Shooting", true);
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
            meshAnimator.SetBool("Shooting", false);
            muzzleFlash.SetActive(false);
        }
    }

    void GunSight()
    {
        if (sighting)
        {
            parentAnimator.SetBool("GunAim", true);
            aimSound.pitch = 1;
        }
        else
        {
            parentAnimator.SetBool("GunAim", false);
            aimSound.pitch = 0.8f;
        }
            
    }

    void ReloadWeapon()
    {
        if(reloading)
        {
            reloadTime -= Time.deltaTime;
            meshAnimator.speed = 1;
            meshAnimator.SetBool("Reloading", true);

            if(reloadTime > currentWeapon.reloadTime - 0.1f)
                if(!clickSound.isPlaying)
                    clickSound.Play();

            if (reloadTime <= 0)
            {
                if (currentAmmo < totalAmmo)
                {
                    currentWeapon.currentAmmo += (currentWeapon.totalAmmo - currentAmmo);
                }
                if (!clickSound.isPlaying)
                    clickSound.Play();
                meshAnimator.SetBool("Reloading", false);
                reloadTime = currentWeapon.reloadTime;
                reloading = false;
            }
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
