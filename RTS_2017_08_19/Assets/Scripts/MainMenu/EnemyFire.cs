using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

    [SerializeField] float minFireDelay = 0.3f;
    [SerializeField] float maxFireDelay = 2.0f;
    [SerializeField] Transform[] fireSources;
    [SerializeField] Transform[] targets;
    float nextFire;
	
    [SerializeField] GameObject[] projectiles;

    void Start()
    {
        nextFire = Time.time + Random.Range(minFireDelay, maxFireDelay);
    }

    void Update () 
    {
        if (Time.time > nextFire)
        {
            FireProjectile();
            nextFire = Time.time + Random.Range(minFireDelay, maxFireDelay);
        }
	}

    void FireProjectile()
    {
        Transform launcher = fireSources[Random.Range(0,fireSources.Length)];
        Transform target = targets[Random.Range(0, targets.Length+1)];
        GameObject projectile = projectiles[Random.Range(0, projectiles.Length)];
        Instantiate(projectile, launcher.position, Quaternion.LookRotation (target.position - launcher.position));
    }
}
