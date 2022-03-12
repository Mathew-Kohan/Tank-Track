using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField]
    private float _speedBullet = 8f;

    [SerializeField]
    private GameObject _bulletPrefab;
    private GameObject _newBullet;

    private Rigidbody2D _rb2dBullet;
    private bool _isBulletActiv = false;

    [HideInInspector]
    public Vector3 pose { get { return transform.position; } }

    [HideInInspector]
    public Vector2 force;

    private void FixedUpdate()
    {
        if (_newBullet == null)
        {
            InstanceBullet();
            Deactive();
        }
        force = transform.right * _speedBullet;

        if (_isBulletActiv == true)
        {
            BulletTrueRotation();
        }
    }
    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isBulletActiv == false)
        {
            _isBulletActiv = true;
            _rb2dBullet.isKinematic = false;
            _rb2dBullet.velocity = force;
        }
    }

    private void Deactive()
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

    private void InstanceBullet()
    {
        _newBullet = Instantiate(_bulletPrefab, transform.position, transform.rotation, transform);

        _rb2dBullet = GetComponentInChildren<Rigidbody2D>();
    }
}
