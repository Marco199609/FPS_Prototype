using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagLogic : MonoBehaviour
{
    MagData magData;
    GameObject mesh;
    WeaponController weaponController;
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
        if (magSound == null)
            magSound = magData.magSound;
        if (magData.randomCapacity == 0)
            magData.randomCapacity = Random.Range(1, 4);
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
            if (magData.randomCapacity == 1)
                magData.magCapacity =  weaponController.currentWeapon.ammoCapacity / 2;
            else if (magData.randomCapacity == 2)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity;
            else if (magData.randomCapacity == 2)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity * 1.5f;
            else if (magData.randomCapacity == 4)
                magData.magCapacity = weaponController.currentWeapon.ammoCapacity * 2;

            print(magData.magCapacity);
            weaponController.currentWeapon.availableAmmo += (int) magData.magCapacity;
            if (!magSound.GetComponent<AudioSource>().isPlaying)
                magSound.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.5f);
        }
    }

}
