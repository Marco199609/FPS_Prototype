using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    WeaponSounds weaponSounds;
    WeaponReload weaponReload;
    [SerializeField]Animator meshAnimator, parentAnimator;
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
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
        if (weaponReload == null)
            weaponReload = gameObject.GetComponent<WeaponReload>();

        isAutomatic = currentWeapon.isAutomatic;
        currentAmmo = currentWeapon.currentAmmo;

        if (meshAnimator == null || weaponController.weaponChanging)
            meshAnimator = currentWeapon.meshAnimator;
        if (parentAnimator == null || weaponController.weaponChanging)
            parentAnimator = currentWeapon.parentAnimator;
        if (isAutomatic && automaticFireRate <= 0)
            automaticFireRate = currentWeapon.automaticFireRate;
        if (muzzleFlash == null || weaponController.weaponChanging)
            muzzleFlash = currentWeapon.muzzleFlash;
        if (spawnPoint == null || weaponController.weaponChanging)
            spawnPoint = currentWeapon.spawnPoint;
        if (bulletImpactPrefab == null)
            bulletImpactPrefab = weaponController.bulletImpactPrefab;
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
                    if (currentAmmo > 0 && !weaponReload.reloading)
                    {
                        ShootNow();
                    }
                    else if(!weaponReload.reloading)
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

                if (currentAmmo > 0 && !weaponReload.reloading)
                {
                    ShootNow();
                }
                else if(!weaponReload.reloading)
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
        }
        else
        {
            parentAnimator.SetBool("GunAim", false);
            weaponSounds.aimSound.pitch = 0.8f;
        }

    }
}
