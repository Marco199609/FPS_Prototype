using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject currentWeapon, spawnPoint, bulletPrefab, bullet, crossHair, hitPointPrefab,
        muzzleFlash, armPivot, cam, gun;

    [SerializeField] AudioSource aimSound, shootSound;
    RaycastHit hit;
    Ray ray;
    bool shooting;
    float shootingTimer = .1f;

    Animator currentWeaponAnimator, gunAnimator;

    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        currentWeaponAnimator = currentWeapon.GetComponent<Animator>();
        gunAnimator = gun.GetComponent<Animator>();
    }
    // Update is called once per frame
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
            //weaponSlider.transform.localPosition = Vector3.Lerp(weaponSlider.transform.localPosition, new Vector3(weaponSlider.transform.localPosition.x, weaponSlider.transform.localPosition.y, -0.0107f), .7f);
            //currentWeapon.transform.localRotation = Quaternion.Lerp(currentWeapon.transform.rotation, Quaternion.Euler(-10, 0, 0), 1f);

            
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
            /* weaponSlider.transform.localPosition = Vector3.Lerp(weaponSlider.transform.localPosition, new Vector3(weaponSlider.transform.localPosition.x, weaponSlider.transform.localPosition.y, 0.01843036f), .7f);
             currentWeapon.transform.localRotation = Quaternion.Lerp(currentWeapon.transform.rotation, Quaternion.Euler(0, 0, 0), 1f);*/
        }

    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            if (Physics.Raycast(ray, out hit, 500))
            {

                bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100, ForceMode.Impulse);
                Destroy(bullet, .2f);
                GameObject hitPoint = Instantiate(hitPointPrefab, hit.point, hit.collider.transform.localRotation);
                hitPoint.transform.SetParent(hit.collider.transform);
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
