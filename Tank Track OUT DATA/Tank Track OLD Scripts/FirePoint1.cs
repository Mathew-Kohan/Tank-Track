using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint1 : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    private GameObject _newBullet;


    [HideInInspector]
    public Vector3 pose { get { return transform.position; } }

    void Start()
    {
        _newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0), transform);

    }
}
