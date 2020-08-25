using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject spawnPoint, pivotPoint, barrelExplosion, finalExplosionPrefab, burnedTurretPrefab;
    public Material laserMaterial;
    public Animator animator, barrelAnimator;
    public AudioSource turretSound;

    public int turretHealth = 50;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
