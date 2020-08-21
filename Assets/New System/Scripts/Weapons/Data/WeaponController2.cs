using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2 : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public Weapon currentWeapon;
    public Transform weaponHolder;
    public GameObject bulletImpactPrefab;
    public GameObject crossHair;
    WeaponShoot weaponShoot;
    WeaponReload weaponReload;
    WeaponInputs weaponInputs;
    WeaponSounds weaponSounds;
    WeaponChange weaponChange;

    private void Start()
    {
        weaponShoot = gameObject.AddComponent<WeaponShoot>();
        weaponReload = gameObject.AddComponent<WeaponReload>();
        weaponInputs = gameObject.AddComponent<WeaponInputs>();
        weaponSounds = gameObject.AddComponent<WeaponSounds>();
        weaponChange = gameObject.AddComponent<WeaponChange>();
    }
}
