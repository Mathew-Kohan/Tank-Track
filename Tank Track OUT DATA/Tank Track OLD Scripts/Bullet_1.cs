using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_1 : MonoBehaviour
{
    [SerializeField]
    private float _speedBullet = 8f;
    [SerializeField]
    private Rigidbody2D _rb2dBullet;

    private bool _isBulletActiv = false;

    [HideInInspector]
    public Vector3 pose { get { return transform.position; } }

    [HideInInspector]
    public Vector2 force;

    void Start()
    {
        _rb2dBullet = GetComponent<Rigidbody2D>();

        Deactive();
    }
    private void FixedUpdate()
    {
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

    private void OnTriggerEnter2D(Collider2D hitInfoBullet)
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isBulletActiv = true;
            _rb2dBullet.isKinematic = false;
            _rb2dBullet.velocity = transform.right * _speedBullet;           
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
        transform.rotation = Quaternion.AngleAxis(truelook, Vector3.forward);
    }
}
