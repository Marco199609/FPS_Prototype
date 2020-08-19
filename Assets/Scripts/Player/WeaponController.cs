using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject currentWeapon, spawnPoint, crossHair, bulletImpactPrefab,
        muzzleFlash, armPivot, cam, gun;
    [SerializeField] AudioSource aimSound, shootSound;
    RaycastHit hit;
    Ray ray;
    Animator currentWeaponAnimator, gunAnimator;
    Vector3 velocity = Vector3.zero;
    bool shooting;
    float shootingTimer = .1f;

    private void Start()
    {
        currentWeaponAnimator = currentWeapon.GetComponent<Animator>();
        gunAnimator = gun.GetComponent<Animator>();
    }

    void Update()
    {
        WeaponPosition();
        Shoot();
        SoundControl();
    }

    void WeaponPosition()
    {
        if (Input.GetMouseButton(1))
        {
            currentWeaponAnimator.SetBool("RightMouse", true);
            crossHair.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            currentWeaponAnimator.SetBool("RightMouse", false);
            crossHair.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (shooting)
        {
            gunAnimator.SetBool("Shooting", true);
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                shooting = false;
                shootingTimer = 0.1f;
            }
        }
        else
        {
            gunAnimator.SetBool("Shooting", false);
        }
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out hit, 500))
            {
                GameObject bulletImpact = Instantiate(bulletImpactPrefab, hit.point, hit.collider.transform.localRotation);
                bulletImpact.transform.SetParent(hit.collider.transform);
                Destroy(bulletImpact, 10f);

                if(hit.collider.tag == "Turret")
                {
                    hit.collider.GetComponentInParent<Turret>().turretHealth -= 10;
                }
            }
            if(muzzleFlash != null)
                muzzleFlash.GetComponent<ParticleSystem>().Play();
            shooting = true;
        }
        else
        {
            muzzleFlash.GetComponent<ParticleSystem>().Stop();
        }
    }

    void SoundControl()
    {
        if (Input.GetMouseButtonDown(1))
        {
            aimSound.pitch = 1;
            aimSound.Play();
        }

        if (Input.GetMouseButtonUp(1))
        {
            aimSound.pitch = 0.8f;
            aimSound.Play();
        }

        if(Input.GetMouseButtonDown(0))
            shootSound.Play();
    }
}
