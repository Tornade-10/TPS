using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootingSystem : MonoBehaviour
{
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;
    
    [Header("Shooting Value")]
    public float maxShootDistance = 300f;
    
    [Header("Debug and UI")]
    public GameObject rayStart ;
    public GameObject rayEnd ;
    public GameObject CrossHair;
    
    [Header("Bullet")] 
    public GameObject bullet;
    public float bulletVelocity;
    public int bulletPerShot;
    
    [Header("FX And SFX")] 
    public GameObject flash;
    public AudioClip gunShot;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;
        
        flash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (_inputs.isAiming)
        {
            CrossHair.SetActive(true);
        }
        else
        {
            CrossHair.SetActive(false);
        }
        
        if (_inputs.isShooting)
        {
            Vector3 shootPoint = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, maxShootDistance));
            
            flash.SetActive(true);
            
            Ray ray = new Ray(rayStart.transform.position, shootPoint - rayEnd.transform.position);

            audioSource.PlayOneShot(gunShot);
            for (int i = 0; i < bulletPerShot; i++)
            {
                GameObject currentBullet = Instantiate(bullet, rayStart.transform.position, rayStart.transform.rotation);
                Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
                rb.AddForce(rayStart.transform.forward * bulletVelocity, ForceMode.Impulse);
            }
            
            Debug.DrawRay(ray.origin, ray.direction * maxShootDistance, Color.magenta, 0.5f);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxShootDistance))
            {
                rayEnd.transform.position = hitInfo.point;
            }
        }
        else
        {
            flash.SetActive(false);
        }
    }
}
