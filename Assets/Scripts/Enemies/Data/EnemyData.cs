using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Configs for AI")]
    public bool isAIObject;                                           //Adds moving AI if movable object
    public string pathObjectsTag;
    public GameObject[] pathToFollow;
    public float speed, rotateSpeed, minDirTimer, maxDirTimer;


    [Header("Configs for static objects")]
    public bool isStaticRotateObj;
    public Vector3 rotationAxis;

    [Header("Configs for Health")]
    public int health;
    public ParticleSystem explosionPrefab;
    public GameObject burnedEnemy;

    [Header("Effects")]
    public AudioSource attackSound;
    public GameObject muzzleFlash;
    public Animator enemyAnimator;

    [Header("Use if attacking with rays")]
    public bool rayPlayerDetection;
    public LineRenderer lineRenderer;
    public Transform spawnPoint;

    [Header("Use if attacking without rays")]
    public Light enemyLight;
}
