using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagLogic : MonoBehaviour
{
    MagData magData;
    GameObject mesh;
    WeaponController weaponController;
    Weapon currentWeapon;
    GameObject player, magSound;

    void SetVariables()
    {
        if (magData == null)
            magData = gameObject.GetComponent<MagData>();
        if (mesh == null)
            mesh = magData.mesh;
        if (weaponController == null)
            weaponController = GameObject.FindWithTag("WeaponController").GetComponent<WeaponController>();
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (currentWeapon == null || weaponController.weaponChanging)
            currentWeapon = weaponController.currentWeapon;
        if (magSound == null)
            magSound = magData.magSound;

        if(magData.randomCapacity == 0)
        {
            magData.randomCapacity = Random.Range(1, 4);

            if(magData.randomCapacity == 1)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity / 2;
            if (magData.randomCapacity == 2)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity;
            if (magData.randomCapacity == 2)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity * 1.5f;
            if (magData.randomCapacity == 4)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity * 2;
        }
    }

    void FixedUpdate()
    {
        SetVariables();



        mesh.transform.Rotate(magData.rotateAngle);

        if(magData.enableTimer > 0)
        {
            magData.enableTimer -= Time.deltaTime;
        }
        else
        {
            magSound.SetActive(true);
            mesh.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == player.tag)
        {
            currentWeapon.availableAmmo += (int) magData.magCapacity;
            if (!magSound.GetComponent<AudioSource>().isPlaying)
                magSound.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.5f);
        }
    }

}
