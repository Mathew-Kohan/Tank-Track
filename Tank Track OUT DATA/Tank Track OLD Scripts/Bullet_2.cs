using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_2 : MonoBehaviour
{
    [SerializeField]
    private float _speedBullet = 1000f;
    [SerializeField]
    private Rigidbody2D _rb2dBullet;



    [HideInInspector]
    public Vector3 pose { get { return transform.position; } }


    public Vector2 force;

    void Start()
    {
        _rb2dBullet.velocity = transform.right * _speedBullet;
        force = new Vector2(_rb2dBullet.velocity.x, _rb2dBullet.velocity.y);
    }

    private void Update()
    {
        BulletTrueLook();
    }

    private void OnTriggerEnter2D(Collider2D hitInfoBullet)
    {
        Destroy(gameObject);
    }

    private void BulletTrueLook()
    {
        float truelook = Mathf.Atan2(_rb2dBullet.velocity.y, _rb2dBullet.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(truelook, Vector3.forward);
    }
}
