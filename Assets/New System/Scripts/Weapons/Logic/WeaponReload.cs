using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponReload : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    Animator meshAnimator;
    WeaponSounds weaponSounds;
    int ammoCapacity, availableAmmo, currentAmmo;
    float reloadTime;
    public bool reloading;

    void SetVariables()
    {
        if(weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();

        if (ammoCapacity == 0 || weaponController.weaponChanging)
            ammoCapacity = currentWeapon.ammoCapacity;

        currentAmmo = currentWeapon.currentAmmo;
        availableAmmo = currentWeapon.availableAmmo;

        if (reloadTime == 0 || weaponController.weaponChanging)
            reloadTime = currentWeapon.reloadTime;
        if (meshAnimator == null || weaponController.weaponChanging)
            meshAnimator = currentWeapon.meshAnimator;

    }

    private void FixedUpdate()
    {
        SetVariables();
        ReloadWeapon();
    }

    void ReloadWeapon()
    {
        if (reloading)
        {
            reloadTime -= Time.deltaTime;
            meshAnimator.speed = 1;
            meshAnimator.SetBool("Reloading", true);

            

            if (reloadTime <= 0)
            {
                if (currentAmmo < ammoCapacity)
                {
                    if(availableAmmo > ammoCapacity)
                    {
                        currentWeapon.availableAmmo -= (currentWeapon.ammoCapacity - currentAmmo);
                        currentWeapon.currentAmmo += (currentWeapon.ammoCapacity - currentAmmo);
                    }
                    else
                    {
                        currentWeapon.currentAmmo += availableAmmo;
                        currentWeapon.availableAmmo = 0;
                    }

                }

                reloadTime = currentWeapon.reloadTime;
                reloading = false;
            }

           if (!weaponSounds.reloadSound.isPlaying)
                weaponSounds.reloadSound.Play();
        }
        else
            meshAnimator.SetBool("Reloading", false);
    }
}
