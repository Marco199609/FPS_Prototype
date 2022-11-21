using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    WeaponSounds weaponSounds;
    WeaponReload weaponReload;
    Animator meshAnimator;
    GameObject muzzleFlash, bulletImpactPrefab, crossHair;
    Transform aimPoint;
    RaycastHit hit;
    public Ray shootRay;    //Set in WeaponAim
    public bool shooting, autoModeOn, isAutoWeapon;
    float automaticFireRate, aimTime = 0.3f;
    int currentAmmo, weaponRange;

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

        if(currentWeapon != null)
        {
                        autoModeOn = currentWeapon.autoModeOn;
            currentAmmo = currentWeapon.currentAmmo;

        if (weaponController.weaponChanging)
            isAutoWeapon = currentWeapon.isAutoWeapon;

        if (meshAnimator == null || weaponController.weaponChanging)
            meshAnimator = currentWeapon.meshAnimator;
        if (autoModeOn && isAutoWeapon && automaticFireRate <= 0 || isAutoWeapon && automaticFireRate == Mathf.Infinity)
            automaticFireRate = (1 / (currentWeapon.fireRate / 60)); //calculates rate per second
        if (muzzleFlash == null || weaponController.weaponChanging)
            muzzleFlash = currentWeapon.muzzleFlash;
        if (aimPoint == null || weaponController.weaponChanging)
            aimPoint = currentWeapon.aimPoint;
        if (bulletImpactPrefab == null)
            bulletImpactPrefab = weaponController.bulletImpactPrefab;
        if (weaponRange == 0 || weaponController.weaponChanging)
            weaponRange = currentWeapon.rangeInMeters;
        }
    }

    private void FixedUpdate()
    {
        SetVariables();
        ShootWeapon();
    }


    void ShootWeapon()
    {
        if (shooting)
        {
            if (autoModeOn && isAutoWeapon)
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
                    automaticFireRate = (1 / (currentWeapon.fireRate / 60));     //calculates rate per second
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
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        currentWeapon.currentAmmo--;

        weaponSounds.shootSound.pitch = Random.Range(0.9f, 1.2f);
        weaponSounds.shootSound.Play();

        if (Physics.Raycast(shootRay, out hit, weaponRange))
        {
            GameObject bulletImpact = Instantiate(bulletImpactPrefab, hit.point, hit.collider.transform.localRotation);
            bulletImpact.transform.SetParent(hit.collider.transform);
            Destroy(bulletImpact, 10f);

            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponentInParent<EnemyHealth>().health -= currentWeapon.damage;

                if(hit.collider.GetComponentInParent<EnemyHealth>().health <= 0)
                {
                    weaponController.killEnemiesAmmo--;

                    if (weaponController.killEnemiesAmmo <= 0)
                    {
                        Instantiate(weaponController.magPrefab, new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z), Quaternion.identity);
                        weaponController.killEnemiesAmmo = Random.Range(5, 10);
                    }

                }
            }
        }
        meshAnimator.SetBool("Shooting", true);
    }
}
