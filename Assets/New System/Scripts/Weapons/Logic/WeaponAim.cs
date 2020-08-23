using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    Animator parentAnimator;
    WeaponShoot weaponShoot;
    WeaponSounds weaponSounds;
    Transform aimPoint;
    Animator walkAnimator;
    public bool aiming;
    public float camTime;


    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (parentAnimator == null || weaponController.weaponChanging)
            parentAnimator = currentWeapon.parentAnimator;
        if (aimPoint == null || weaponController.weaponChanging)
            aimPoint = currentWeapon.aimPoint;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
        if (weaponShoot == null || weaponController.weaponChanging)
            weaponShoot = gameObject.GetComponent<WeaponShoot>();
        if (walkAnimator == null)
            walkAnimator = weaponController.walkAnimator;
    }


    void FixedUpdate()
    {
        SetVariables();
        Aim();
    }


    void Aim()
    {
        if (aiming)
        {
            if (camTime < 1)
                camTime += Time.deltaTime * 6f;

            Camera.main.fieldOfView = Mathf.Lerp(60, 30, camTime);
            weaponShoot.shootRay = new Ray(aimPoint.position, aimPoint.transform.position - Camera.main.transform.position);

            parentAnimator.SetBool("GunAim", true);
            weaponSounds.aimSound.pitch = 1;

            if (walkAnimator != null)
                walkAnimator.speed = 0.3f;
        }
        else
        {
            if (camTime > 0)
                camTime -= Time.deltaTime * 6f;

            Camera.main.fieldOfView = Mathf.Lerp(60, 30, camTime);
            weaponShoot.shootRay = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            parentAnimator.SetBool("GunAim", false);
            weaponSounds.aimSound.pitch = 0.8f;

            if (walkAnimator != null)
                walkAnimator.speed = 1f;
        }

    }
}
