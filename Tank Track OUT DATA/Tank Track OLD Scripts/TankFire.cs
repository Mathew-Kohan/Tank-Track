using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    //--Private Variables Exposed to the Inspector
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private GameObject _bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
