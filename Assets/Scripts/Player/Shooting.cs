using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int bulletCount = 4;

    [SerializeField]
    private float _speedBullet = 12f;

    [SerializeField]
    private float _fireRate = 1f;

    private float _nextFire = 0f;

    [SerializeField]
    private GameObject _firePoint;
    [SerializeField]
    private GameObject _bulletPrefab;


    private Rigidbody2D _rb2dBullet;
    private Rigidbody2D _newrb2dBullet;


    public bool isBulletActiv = false;

    [HideInInspector]
    public Vector3 pose { get { return _firePoint.transform.position; } }

    [HideInInspector]
    public Vector2 force;

    private void Start()
    {
        _rb2dBullet = _bulletPrefab.GetComponent<Rigidbody2D>();

        UIManager.Instance.BulletCount(bulletCount);
    }
    private void FixedUpdate()
    {
        if (bulletCount >= 0)
        {
            force = _firePoint.transform.right * _speedBullet;  
        }

        if (_newrb2dBullet != null)
        {
            BulletTrueRotation();
        }

        if (isBulletActiv == true && Time.time > _nextFire)
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        bulletCount--;
        UIManager.Instance.BulletCount(bulletCount);
        isBulletActiv = false;
        _nextFire = Time.time + _fireRate;

        _newrb2dBullet = Instantiate(_rb2dBullet, _firePoint.transform.position, _firePoint.transform.rotation);
        _newrb2dBullet.velocity = force;
    }

    private void BulletTrueRotation()
    {
        float truelook = Mathf.Atan2(_newrb2dBullet.velocity.y, _newrb2dBullet.velocity.x) * Mathf.Rad2Deg;

        _newrb2dBullet.rotation = truelook;
    }
}
