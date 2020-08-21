using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInputs : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    WeaponShoot weaponShoot;
    WeaponReload weaponReload;
    WeaponSounds weaponSounds;
    WeaponChange weaponChange;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if (weaponShoot == null)
            weaponShoot = gameObject.GetComponent<WeaponShoot>();
        if (weaponReload == null)
            weaponReload = gameObject.GetComponent<WeaponReload>();
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
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
            if (currentWeapon.isAutomatic)
                weaponShoot.shooting = false;

        if (Input.GetKeyDown(KeyCode.R))
            weaponReload.reloading = true;

        if (Input.GetMouseButtonDown(1))
        {
            weaponShoot.aiming = true;
            weaponSounds.aimSound.Play();
        }
        if (Input.GetMouseButtonUp(1))
        {
            weaponShoot.aiming = false;
            weaponSounds.aimSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponSounds.clickSound.Play();
            if (currentWeapon.isAutomatic)
            {
                currentWeapon.isAutomatic = false;
                weaponController.autoText.color = new Color(0.03003764f, 1, 0, 0.2f);
            }
            else
            {
                currentWeapon.isAutomatic = true;
                weaponController.autoText.color = new Color(0.03003764f, 1, 0, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponChange.weaponIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponChange.weaponIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            weaponChange.weaponIndex = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            weaponChange.weaponIndex = 4;
    }
}
