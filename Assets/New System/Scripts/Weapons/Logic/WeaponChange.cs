using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    WeaponController2 weaponController;
    Weapon currentWeapon;
    public int weaponIndex = 1;
    [SerializeField] GameObject[] weaponPrefabs, weaponsAvailable;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController2>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
    }

    private void Start()
    {
        weaponsAvailable = new GameObject[weaponPrefabs.Length];
        for (int i = 0; i < weaponsAvailable.Length; i++)
        {
            weaponsAvailable[i] = Instantiate(weaponPrefabs[i], transform.position, Quaternion.identity);
            weaponsAvailable[i].transform.SetParent(weaponController.weaponHolder);
        }
    }
    void Update()
    {
        SetVariables();
        Change();
    }

    void Change()
    {

    }
}
