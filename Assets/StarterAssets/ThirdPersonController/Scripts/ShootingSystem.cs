using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{

    [SerializeField] private GameObject _shootingTarget ;
    [SerializeField] private GameObject _target ;
    [SerializeField] private float _maxShootDistance = 300f;
        
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;
    
    
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
            _shootingTarget.SetActive(true);
            _target.SetActive(true);
            
            Vector3 shootPoint = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _maxShootDistance));
            
            Ray ray = new Ray(_shootingTarget.transform.position, shootPoint - _target.transform.position);
            
            Debug.DrawRay(ray.origin, ray.direction * _maxShootDistance, Color.magenta, 0.5f);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxShootDistance))
            {
                _target.transform.position = hitInfo.point;
                hitInfo.collider.gameObject.CompareTag("Target");
            }
        }
        else
        {
            _shootingTarget.SetActive(false);
            _target.SetActive(false);
        }
    }
}
