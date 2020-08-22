using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSounds : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    public AudioSource shootSound, aimSound, clickSound, magSlideSound, reloadSound, changingSound;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (shootSound == null || weaponController.weaponChanging)
            shootSound = currentWeapon.shootSound;
        if (aimSound == null || weaponController.weaponChanging)
            aimSound = currentWeapon.aimSound;
        if (clickSound == null || weaponController.weaponChanging)
            clickSound = currentWeapon.clickSound;
        if (magSlideSound == null || weaponController.weaponChanging)
            magSlideSound = currentWeapon.magSlideSound;
        if (reloadSound == null || weaponController.weaponChanging)
            reloadSound = currentWeapon.reloadSound;
        if (changingSound == null)
            changingSound = weaponController.changingSound;
    }

    void Update()
    {
        SetVariables();
    }
}
