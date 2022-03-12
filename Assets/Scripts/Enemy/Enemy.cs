using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _firePoint;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _explosion;


    [SerializeField]
    private int _health;
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private HealthBar _healthBar;

    [SerializeField]
    private Transform _turretTransform;

    [Header("AI Attributes")]
    [SerializeField]
    private float _fireRate = 1f;
    [SerializeField]
    private float _actionDistance = 10f;
    [SerializeField]
    private float _attackDistance = 5f;

    private float _speed = 1f;
    private float _targetDistance;
    private float _nextFire = 0f;



    private Rigidbody2D _rigid2d;
    private Rigidbody2D _rb2dBullet;
    private Rigidbody2D _newrb2dBullet;

    private Vector3 force;

    private void Start()
    {
        _health = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        Health = _health;
        _healthBar.SetHealth(_health);

        _rigid2d = GetComponent<Rigidbody2D>();
        _rb2dBullet = _bulletPrefab.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        CalculateTarget();
        Move();
    }

    private void CalculateTarget()
    {
        if (_player != null)
        {
            _targetDistance = Vector3.Distance(_player.transform.position, transform.position);

            if (_targetDistance < _actionDistance || Input.GetKeyDown(KeyCode.E))
            {
                Aiming();
            }

            if (_targetDistance < _attackDistance && Time.time > _nextFire || Input.GetKeyDown(KeyCode.Q))
            {
                Shoot();
            }
        }

        if (_newrb2dBullet != null)
        {
            BulletTrueRotation();
        }
    }

    Vector3 CalculateVelocity (Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    private void Aiming()
    {
        force = CalculateVelocity(_player.transform.position, _turretTransform.position, 1f);

        Quaternion forceRotaion = Quaternion.LookRotation(force);
        _turretTransform.rotation = Quaternion.Euler(0, 0, forceRotaion.eulerAngles.x);
    }

    private void Shoot()
    {
        _nextFire = Time.time + _fireRate;

        _newrb2dBullet = Instantiate(_rb2dBullet, _firePoint.transform.position, Quaternion.identity);
        _newrb2dBullet.velocity = force;
    }

    private void BulletTrueRotation()
    {
        float truelook = Mathf.Atan2(_newrb2dBullet.velocity.y, _newrb2dBullet.velocity.x) * Mathf.Rad2Deg;

        _newrb2dBullet.rotation = truelook;
    }

    private void StableAngleTank()
    {
        Vector3 currentRotation = transform.eulerAngles;
        if (currentRotation.z > 180)
        {
            currentRotation.z -= 360;
        }
        currentRotation.z = Mathf.Clamp(currentRotation.z, -5f, 45f);
        transform.eulerAngles = currentRotation;
    }

    private void Move()
    {
        if (_targetDistance < _actionDistance)
        {
            _speed = 1;          
        }
        if(_targetDistance < _attackDistance)
        {
            _speed = 0;
        }

        if (_speed == 0f)
        {
            _rigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            _rigid2d.constraints = RigidbodyConstraints2D.None;
        }

        if (_player != null)
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);

        _speed = 0f;
        StableAngleTank();
    }

    public void Damage()
    {
        _health--;
        _healthBar.SetHealth(_health);

        GameObject explode = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explode, 2f);

        if (_health < 1)
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
