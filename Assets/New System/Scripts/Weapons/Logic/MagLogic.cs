using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagLogic : MonoBehaviour
{
    MagData magData;
    GameObject mesh;
    WeaponController weaponController;
    Weapon currentWeapon;
    GameObject player;
    AudioSource magSound;


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
    }


    void Update()
    {
        SetVariables();
        mesh.transform.Rotate(magData.rotateAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == player.tag)
        {
            currentWeapon.availableAmmo += magData.magCapacity;
            if (!magSound.isPlaying)
                magSound.Play();
            Destroy(gameObject, 0.5f);
        }
    }

}
