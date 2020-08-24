using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weaponPrefabs, ActiveWeaponHUD;
    public Weapon currentWeapon;
    public Transform weaponHolder;
    public GameObject bulletImpactPrefab, player, magPrefab, availableAmmoSprite;
    public Text autoText, reloadText, availableAmmoText;
    public bool weaponChanging;
    public AudioSource changingSound;
    public Animator walkAnimator;
    public int killEnemiesAmmo = 1;         //Amount of enemies to kill to recieve more ammo; used in WeaponShoot.ShootNow();

    [Header("Classes to be added")]
    [SerializeField] WeaponShoot weaponShoot;
    [SerializeField] WeaponAim weaponAim;
    [SerializeField] WeaponReload weaponReload;
    [SerializeField] WeaponInputs weaponInputs;
    [SerializeField] WeaponSounds weaponSounds;
    [SerializeField] WeaponChange weaponChange;
    [SerializeField] WeaponHUD weaponHUD;

    private void Awake()
    {
        weaponShoot = gameObject.AddComponent<WeaponShoot>();
        weaponAim = gameObject.AddComponent<WeaponAim>();
        weaponReload = gameObject.AddComponent<WeaponReload>();
        weaponInputs = gameObject.AddComponent<WeaponInputs>();
        weaponSounds = gameObject.AddComponent<WeaponSounds>();
        weaponChange = gameObject.AddComponent<WeaponChange>();
        weaponHUD = gameObject.AddComponent<WeaponHUD>();

        if(player == null)
            player = GameObject.FindWithTag("Player");
    }
}
