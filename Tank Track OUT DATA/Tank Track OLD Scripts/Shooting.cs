using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float _speedBullet = 12f;

    public int bulletCount = 4;

    [SerializeField]
    private GameObject _firePoint;
    [SerializeField]
    private GameObject _bulletPrefab;

    private GameObject _newBullet;


    private Rigidbody2D _rb2dBullet;

    private bool _isBulletActiv = false;

    [HideInInspector]
    public Vector3 pose { get { return _firePoint.transform.position; } }

    [HideInInspector]
    public Vector2 force;

    private void Update()
    {
        UIManager.Instance.BulletCount(bulletCount);
    }
    private void FixedUpdate()
    {
        if (bulletCount >= 0)
        {
            if (_newBullet == null)
            {
                InstanceBullet();
                DeactiveRigid();
            }

            force = _firePoint.transform.right * _speedBullet;

            if (_isBulletActiv == true)
            {
                BulletTrueRotation();
            }
        }
    }

    private void InstanceBullet()
    {
         _newBullet = Instantiate(_bulletPrefab, _firePoint.transform.position, _firePoint.transform.rotation, _firePoint.transform);

         _rb2dBullet = _newBullet.GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        if (_isBulletActiv == false && bulletCount >= 1)
        {
            bulletCount--;

            _isBulletActiv = true;
            _rb2dBullet.isKinematic = false;
            _rb2dBullet.velocity = force;
        }
    }

    private void DeactiveRigid()
    {
        _isBulletActiv = false;
        _rb2dBullet.velocity = Vector3.zero;
        _rb2dBullet.angularVelocity = 0f;
        _rb2dBullet.isKinematic = true;
    }

    private void BulletTrueRotation()
    {
        float truelook = Mathf.Atan2(_rb2dBullet.velocity.y, _rb2dBullet.velocity.x) * Mathf.Rad2Deg;
        _newBullet.transform.rotation = Quaternion.AngleAxis(truelook, Vector3.forward);
    }
}
