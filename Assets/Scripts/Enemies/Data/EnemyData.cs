using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Configs for AI")]
    public GameObject[] pathToFollow;
    public bool isAIObject;
    public float speed, rotateSpeed, minDirTimer, maxDirTimer;

    [Header("Configs for Health")]
    public int health;
    public ParticleSystem explosionPrefab;
    public GameObject burnedEnemy;
}
