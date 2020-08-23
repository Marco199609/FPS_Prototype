﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponHUD : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    WeaponReload weaponReload;
    float textTransparency;
    bool transparentText;
    int currentAmmo, ammoCapacity, availableAmmo;
    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (weaponReload == null)
            weaponReload = gameObject.GetComponent<WeaponReload>();
        if (ammoCapacity == 0 || weaponController.weaponChanging)
            ammoCapacity = currentWeapon.ammoCapacity;
        currentAmmo = currentWeapon.currentAmmo;
        availableAmmo = currentWeapon.availableAmmo;
    }

    void Update()
    {
        SetVariables();
        ReloadText();
        AvailableAmmoText();
        AutoMode();
    }

    void ReloadText()
    {
        if (weaponReload.reloading && weaponController.reloadText.isActiveAndEnabled)
        {
            weaponController.reloadText.color = new Color(0.3f, 1, 0, 1);
            weaponController.reloadText.text = "Reloading...";
        }
        else if (!weaponReload.reloading && weaponController.reloadText.isActiveAndEnabled)
        {
            if (textTransparency <= 0.2f)
                transparentText = false;
            if (textTransparency >= 1)
                transparentText = true;

            if (transparentText)
                textTransparency -= Time.deltaTime * 1f;
            else
                textTransparency += Time.deltaTime * 1f;

            weaponController.reloadText.color = new Color(1, 0, 0, textTransparency);
        }


        if (currentAmmo <= ammoCapacity / 3 && availableAmmo != 0 || weaponReload.reloading)
        {
            weaponController.reloadText.enabled = true;
        }
        else if (availableAmmo == 0 && currentAmmo == 0)
        {
            weaponController.reloadText.text = "No ammo";
            weaponController.reloadText.enabled = true;
        }
        else
        {
            weaponController.reloadText.enabled = false;
            weaponController.reloadText.text = "Reload";
            textTransparency = 1;
        }

    }

    void AvailableAmmoText()
    {
        weaponController.availableAmmoText.text = currentAmmo + "/" + availableAmmo;

        if (currentAmmo < ammoCapacity / 3 || availableAmmo < ammoCapacity)
            weaponController.availableAmmoText.color = new Color(1, 0, 0, textTransparency);
        else
            weaponController.availableAmmoText.color = new Color(0.3f, 1, 0, 1);

    }


    void AutoMode()
    {
        if (!currentWeapon.isAutoWeapon)
        {
            weaponController.autoText.text = "No Auto";
            weaponController.autoText.color = new Color(1, 0, 0, 0.5f);
        }
        else
        {
            weaponController.autoText.text = "Auto";

            if (currentWeapon.autoModeOn)
                weaponController.autoText.color = new Color(0.03003764f, 1, 0, 1);
            else
                weaponController.autoText.color = new Color(0.03003764f, 1, 0, 0.3f);
        }

    }
}
