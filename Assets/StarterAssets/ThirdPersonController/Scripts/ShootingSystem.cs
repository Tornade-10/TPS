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
    
    [Header("Start And End of Ray")]
    public GameObject rayStart ;
    public GameObject rayEnd ;

    [Header("The Bullet")] 
    public GameObject bullet;
    public float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.isShooting)
        {
            rayStart.SetActive(true);
            rayEnd.SetActive(true);
            
            Vector3 shootPoint = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, maxShootDistance));
            
            Ray ray = new Ray(rayStart.transform.position, shootPoint - rayEnd.transform.position);

            Bullet();
            
            Debug.DrawRay(ray.origin, ray.direction * maxShootDistance, Color.magenta, 0.5f);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxShootDistance))
            {
                rayEnd.transform.position = hitInfo.point;
                //hitInfo.collider.gameObject.CompareTag("Target");
            }
        }
        else
        {
            rayStart.SetActive(false);
            rayEnd.SetActive(false);
        }
    }
    
    void Bullet()
    {
        Instantiate(bullet, rayStart.transform.position, rayEnd.transform.rotation);
    }
    
}
