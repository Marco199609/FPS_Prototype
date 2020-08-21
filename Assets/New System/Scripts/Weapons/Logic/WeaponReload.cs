using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponReload : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    Animator meshAnimator;
    WeaponSounds weaponSounds;
    int ammoCapacity, availableAmmo, currentAmmo;
    float reloadTime;
    public bool reloading;

    void SetVariables()
    {
        if(weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
        if (ammoCapacity == 0)
            ammoCapacity = currentWeapon.ammoCapacity;
        currentAmmo = currentWeapon.currentAmmo;
        availableAmmo = weaponController.availableAmmo;

        if (reloadTime == 0)
            reloadTime = currentWeapon.reloadTime;
        if (meshAnimator == null)
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
            ReloadAudio();

            if (reloadTime <= 0)
            {
                if (currentAmmo < ammoCapacity)
                {
                    if(availableAmmo > ammoCapacity)
                    {
                        weaponController.availableAmmo -= (currentWeapon.ammoCapacity - currentAmmo);
                        currentWeapon.currentAmmo += (currentWeapon.ammoCapacity - currentAmmo);
                    }
                    else
                    {
                        currentWeapon.currentAmmo += availableAmmo;
                        weaponController.availableAmmo = 0;
                    }

                }


                meshAnimator.SetBool("Reloading", false);
                reloadTime = currentWeapon.reloadTime;
                reloading = false;
            }
        }
    }

    void ReloadAudio()
    {
        if (reloadTime < currentWeapon.clickSoundTime + 0.1f && reloadTime > currentWeapon.clickSoundTime)
            if (!weaponSounds.clickSound.isPlaying)
                weaponSounds.clickSound.Play();
        if (reloadTime < currentWeapon.magSoundTime + 0.1f && reloadTime > currentWeapon.magSoundTime)
            if (!weaponSounds.magSlideSound.isPlaying)
                weaponSounds.magSlideSound.Play();
        if (reloadTime < currentWeapon.reloadSoundTime + 0.1f && reloadTime > currentWeapon.reloadSoundTime)
            if (!weaponSounds.reloadSound.isPlaying)
                weaponSounds.reloadSound.Play();
    }
}
