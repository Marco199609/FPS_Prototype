using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHUD : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    WeaponReload weaponReload;
    float textTransparency;
    bool transparentText;
    int currentAmmo, ammoCapacity, availableAmmo;
    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if (weaponReload == null)
            weaponReload = gameObject.GetComponent<WeaponReload>();
        if (ammoCapacity == 0)
            ammoCapacity = currentWeapon.ammoCapacity;
        currentAmmo = currentWeapon.currentAmmo;
        availableAmmo = weaponController.availableAmmo;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetVariables();
        ReloadText();
        AvailableAmmoText();
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

}
