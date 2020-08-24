using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject spawnPoint, pivotPoint, barrelExplosion, finalExplosionPrefab, burnedTurretPrefab;
    [SerializeField] Material laserMaterial;
    [SerializeField] Animator animator, barrelAnimator;
    [SerializeField] AudioSource turretSound;

    public int turretHealth = 50;
    RaycastHit hit;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        laserMaterial = lineRenderer.material;
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
        HealthControl();
    }

    

    void Shoot()
    {
        Ray ray = new Ray(spawnPoint.transform.position, spawnPoint.transform.forward);

        //Ray ray = new Ray(spawnPoint.transform.position, spawnPoint.transform.rotation * new Vector3(0.5f, 0, 1));
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(2, ray.origin);
        lineRenderer.SetPosition(4, ray.origin);
        lineRenderer.SetPosition(6, ray.origin);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            float randomRays = Random.Range(0.5f, 1f);
            float randomRays2 = Random.Range(0.5f, 1f);

            lineRenderer.SetPosition(1, new Vector3(hit.point.x - randomRays2, hit.point.y - randomRays, hit.point.z));
            lineRenderer.SetPosition(3, new Vector3(hit.point.x - randomRays, hit.point.y + randomRays2, hit.point.z));
            lineRenderer.SetPosition(5, new Vector3(hit.point.x + randomRays2, hit.point.y - randomRays, hit.point.z));
            lineRenderer.SetPosition(7, new Vector3(hit.point.x, hit.point.y - randomRays, hit.point.z));

            if (hit.collider.tag == "Player")
            {
                laserMaterial.SetColor("_EmissionColor", Color.red);

                pivotPoint.transform.LookAt(new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z));

                animator.enabled = false;

                if (!turretSound.isPlaying)
                    turretSound.Play();

                if (barrelAnimator != null)
                    barrelAnimator.SetBool("Shooting", true);

                if (barrelExplosion != null)
                    barrelExplosion.SetActive(true);

                hit.collider.GetComponent<PlayerController>().playerHealth -= Time.deltaTime * 8;
            }
            else
            {
                if (barrelExplosion != null)
                    barrelExplosion.SetActive(false);

                laserMaterial.SetColor("_EmissionColor", Color.green);

                //animator.enabled = true;

                if (turretSound.isPlaying)
                    turretSound.Stop();

                if (barrelAnimator != null)
                    barrelAnimator.SetBool("Shooting", false);
            }
        }
        else
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * 200));
    }
    void HealthControl()
    {
        if(turretHealth <= 0)
        {
            var explosion = Instantiate(finalExplosionPrefab, pivotPoint.transform.position, Quaternion.identity);
            Instantiate(burnedTurretPrefab, pivotPoint.transform.position, pivotPoint.transform.rotation);
            Destroy(explosion, 5f);
            Destroy(gameObject);
        }
    }
}
