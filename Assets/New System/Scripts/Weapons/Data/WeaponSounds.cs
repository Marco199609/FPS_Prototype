using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSounds : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    public AudioSource shootSound, aimSound, clickSound, magSlideSound, reloadSound;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if (shootSound == null)
            shootSound = currentWeapon.shootSound;
        if (aimSound == null)
            aimSound = currentWeapon.aimSound;
        if (clickSound == null)
            clickSound = currentWeapon.clickSound;
        if (magSlideSound == null)
            magSlideSound = currentWeapon.magSlideSound;
        if (reloadSound == null)
            reloadSound = currentWeapon.reloadSound;
    }

    void Update()
    {
        SetVariables();
    }
}
