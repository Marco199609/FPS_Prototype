using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetection : MonoBehaviour
{
    public bool playerDetected;

    public void DetectionWithRays(LineRenderer lineRenderer, Transform spawnPoint)
    {
        RaycastHit hit;
        Ray ray = new Ray(spawnPoint.transform.position, spawnPoint.transform.forward);
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
                playerDetected = true;
            }
            else
            {
                playerDetected = false;
            }
        }
        else
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * 200));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 500))                    //Checks if player out of drone view
            {
                if (hit.collider == other)
                {
                    playerDetected = true;
                }
                else
                    playerDetected = false;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
        }
    }
}
