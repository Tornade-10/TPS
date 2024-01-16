using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
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
    private StarterAssetsInputs _inputs;

    public float lastShootTime;

    // Update is called once per frame
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_inputs.isShooting)
        {
            Shoot();
        }
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

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
        Instantiate(impactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
        
        Destroy(trail.gameObject, trail.time);
    }
}

