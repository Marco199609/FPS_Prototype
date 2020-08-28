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
        if (Physics.Raycast(ray, out hit, 200))
        {
            lineRenderer.SetPosition(1, hit.point);

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
