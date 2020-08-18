using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] LineRenderer lineRenderer;
    RaycastHit hit;
    [SerializeField] GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(spawnPoint.transform.position, spawnPoint.transform.forward);
        lineRenderer.SetPosition(0, ray.origin);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * 200));

        //Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.red, 500);


        //lineRenderer.transform.Rotate(0, 1, 10);
    }
}
