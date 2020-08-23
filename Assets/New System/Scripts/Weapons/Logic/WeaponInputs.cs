using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInputs : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    WeaponShoot weaponShoot;
    WeaponReload weaponReload;
    WeaponSounds weaponSounds;
    WeaponChange weaponChange;
    WeaponAim weaponAim;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (weaponShoot == null || weaponController.weaponChanging)
            weaponShoot = gameObject.GetComponent<WeaponShoot>();
        if (weaponReload == null || weaponController.weaponChanging)
            weaponReload = gameObject.GetComponent<WeaponReload>();
        if (weaponSounds == null || weaponController.weaponChanging)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
        if (weaponChange == null || weaponController.weaponChanging)
            weaponChange = gameObject.GetComponent<WeaponChange>();
        if (weaponAim == null)
            weaponAim = gameObject.GetComponent<WeaponAim>();
    }

    private void Update()
    {
        SetVariables();
        InputControl();
    }



    void InputControl()
    {
        if (Input.GetMouseButtonDown(0))
            weaponShoot.shooting = true;

        if (Input.GetMouseButtonUp(0))
            if (currentWeapon.autoModeOn)
                weaponShoot.shooting = false;




        if (Input.GetMouseButtonDown(1))
        {
            weaponAim.aiming = true;
            weaponSounds.aimSound.Play();
        }
        if (Input.GetMouseButtonUp(1))
        {
            weaponAim.aiming = false;
            weaponSounds.aimSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeapon.currentAmmo != currentWeapon.ammoCapacity && currentWeapon.availableAmmo != 0)
                weaponReload.reloading = true;
            else
                weaponSounds.clickSound.Play();
        }




        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponSounds.clickSound.Play();
            if (currentWeapon.autoModeOn)
                currentWeapon.autoModeOn = false;
            else
                currentWeapon.autoModeOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponController.weaponChanging = true;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                weaponChange.weaponIndex = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                weaponChange.weaponIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                weaponChange.weaponIndex = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                weaponChange.weaponIndex = 3;
        }
    }
}
