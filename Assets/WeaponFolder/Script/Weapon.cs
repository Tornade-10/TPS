using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{

    public bool bulletSpread = true;
    public Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    public ParticleSystem muzzleFlashParticleSystem;
    public Transform bulletSpawnPoint;
    public ParticleSystem impactParticleSystem;
    public TrailRenderer bulletTraill;
    public float shootDelay = 0.5f;

    public float lastShootTime;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (lastShootTime + shootDelay < Time.time)
        {
            Vector3 direction = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue))
            {
                TrailRenderer trail = Instantiate(bulletTraill, bulletSpawnPoint.position, quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                lastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (bulletSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }
}

