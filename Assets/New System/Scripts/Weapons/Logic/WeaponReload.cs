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
    int totalAmmo, currentAmmo;
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

        currentAmmo = currentWeapon.currentAmmo;

        if (reloadTime == 0)
            reloadTime = currentWeapon.reloadTime;
        if (meshAnimator == null)
            meshAnimator = currentWeapon.meshAnimator;
        if (totalAmmo == 0)
            totalAmmo = currentWeapon.totalAmmo;
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
                if (currentAmmo < totalAmmo)
                {
                    currentWeapon.currentAmmo += (currentWeapon.totalAmmo - currentAmmo);
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
            if (!currentWeapon.clickSound.isPlaying)
                weaponSounds.clickSound.Play();
        if (reloadTime < currentWeapon.magSoundTime + 0.1f && reloadTime > currentWeapon.magSoundTime)
            if (!currentWeapon.magSlideSound.isPlaying)
                weaponSounds.magSlideSound.Play();
        if (reloadTime < currentWeapon.reloadSoundTime + 0.1f && reloadTime > currentWeapon.reloadSoundTime)
            if (!currentWeapon.reloadSound.isPlaying)
                weaponSounds.reloadSound.Play();
    }
}
