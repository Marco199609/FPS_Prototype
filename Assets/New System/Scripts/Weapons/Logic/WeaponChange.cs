using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    WeaponController weaponController;
    Weapon currentWeapon;
    WeaponSounds weaponSounds;
    public int weaponIndex = 2;
    float weaponChangeTimer = 0.1f;
    [SerializeField] GameObject[] weaponPrefabs, weaponsAvailable;
    Transform weaponHolder;

    void SetVariables()
    {
        if (weaponController == null)
            weaponController = gameObject.GetComponent<WeaponController>();
        if (currentWeapon == null)
            currentWeapon = weaponController.currentWeapon;
        if(weaponPrefabs == null)
            weaponPrefabs = weaponController.weaponPrefabs;
        if (weaponHolder == null)
            weaponHolder = weaponController.weaponHolder;
        if (weaponSounds == null)
            weaponSounds = gameObject.GetComponent<WeaponSounds>();
        if(weaponsAvailable == null)
        {
            weaponsAvailable = new GameObject[weaponPrefabs.Length];
            for (int i = 0; i < weaponsAvailable.Length; i++)
            {
                weaponsAvailable[i] = Instantiate(weaponPrefabs[i], transform.position, Quaternion.identity);
                weaponsAvailable[i].transform.SetParent(weaponHolder);
                weaponsAvailable[i].transform.localPosition = weaponsAvailable[i].GetComponent<Weapon>().initialOffset;
                weaponsAvailable[i].transform.localRotation = Quaternion.Euler(weaponsAvailable[i].GetComponent<Weapon>().initialOffset);
                weaponsAvailable[i].SetActive(false);

                if (i == weaponIndex)
                    weaponController.weaponChanging = true;

            }
        }
    }

    void FixedUpdate()
    {
        SetVariables();
        Change();
    }

    void Change()
    {
        if(weaponController.weaponChanging)
        {


            for (int i = 0; i < weaponsAvailable.Length; i++)
            {
                if (i == weaponIndex && !weaponsAvailable[i].activeInHierarchy)
                {
                    weaponsAvailable[i].SetActive(true);

                                if (!weaponSounds.changingSound.isPlaying)
            {
                weaponSounds.changingSound.pitch = Random.Range(0.9f, 1.1f);
                weaponSounds.changingSound.Play();
            }

                }
                else if (i == weaponIndex && weaponsAvailable[i].activeInHierarchy) { }
                else
                {
                    weaponsAvailable[i].SetActive(false);
                }
            }

            weaponController.currentWeapon = weaponsAvailable[weaponIndex].GetComponent<Weapon>();


            weaponChangeTimer -= Time.deltaTime;
            if (weaponChangeTimer <= 0)
            {
                weaponChangeTimer = 0.3f;
                weaponController.currentWeapon = weaponsAvailable[weaponIndex].GetComponent<Weapon>();
                weaponController.weaponChanging = false;


            }


        }
    }
}
