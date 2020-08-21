using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    WeaponSounds weaponSounds;
    Animator meshAnimator, parentAnimator;
    GameObject muzzleFlash, bulletImpactPrefab, crossHair;
    Transform spawnPoint;
    RaycastHit hit;
    Ray ray;
    public bool shooting, isAutomatic, aiming;
    float automaticFireRate;
    int currentAmmo;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();

        isAutomatic = currentWeapon.isAutomatic;
        currentAmmo = currentWeapon.currentAmmo;

        if (meshAnimator == null)
            meshAnimator = currentWeapon.meshAnimator;
        if (parentAnimator == null)
            parentAnimator = currentWeapon.parentAnimator;
        if (isAutomatic && automaticFireRate <= 0)
            automaticFireRate = currentWeapon.automaticFireRate;
        if (muzzleFlash == null)
            muzzleFlash = currentWeapon.muzzleFlash;
        if (spawnPoint == null)
            spawnPoint = currentWeapon.spawnPoint;
        if (bulletImpactPrefab == null)
            bulletImpactPrefab = weaponController.bulletImpactPrefab;
        if (crossHair == null)
            crossHair = weaponController.crossHair;
    }

    private void FixedUpdate()
    {
        SetVariables();
        ShootWeapon();
        WeaponAim();
    }

    void ShootWeapon()
    {
        if (shooting)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            if (isAutomatic)
            {
                meshAnimator.speed = 1 / (automaticFireRate * 10);
                automaticFireRate -= Time.fixedDeltaTime;

                if (automaticFireRate <= 0)
                {
                    if (currentAmmo > 0)
                    {
                        ShootNow();
                    }
                    else
                    {
                        meshAnimator.SetBool("Shooting", false);
                        muzzleFlash.GetComponent<ParticleSystem>().Stop();

                        if (!weaponSounds.clickSound.isPlaying)
                            weaponSounds.clickSound.Play();
                    }
                    automaticFireRate = currentWeapon.automaticFireRate;
                }
                else
                {
                    muzzleFlash.GetComponent<ParticleSystem>().Stop();
                }
            }
            else
            {
                meshAnimator.speed = 2;

                if (currentAmmo > 0)
                {
                    ShootNow();
                }
                else
                {
                    if (!weaponSounds.clickSound.isPlaying)
                        weaponSounds.clickSound.Play();
                }
                shooting = false;
            }
        }
        else
        {
            meshAnimator.SetBool("Shooting", false);
            muzzleFlash.GetComponent<ParticleSystem>().Stop();
        }
    }

    void ShootNow()
    {
        meshAnimator.SetBool("Shooting", true);
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        currentWeapon.currentAmmo--;

        weaponSounds.shootSound.pitch = Random.Range(0.9f, 1.2f);
        weaponSounds.shootSound.Play();

        if (Physics.Raycast(ray, out hit, 500))
        {
            GameObject bulletImpact = Instantiate(bulletImpactPrefab, hit.point, hit.collider.transform.localRotation);
            bulletImpact.transform.SetParent(hit.collider.transform);
            Destroy(bulletImpact, 10f);

            if (hit.collider.tag == "Turret")
            {
                hit.collider.GetComponentInParent<Turret>().turretHealth -= 10;
            }
        }
    }

    void WeaponAim()
    {
        if (aiming)
        {
            parentAnimator.SetBool("GunAim", true);
            weaponSounds.aimSound.pitch = 1;
            //crossHair.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            parentAnimator.SetBool("GunAim", false);
            weaponSounds.aimSound.pitch = 0.8f;
            //crossHair.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
