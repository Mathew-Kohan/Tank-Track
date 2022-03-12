using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    //--Private Variables Exposed to the Inspector
    [SerializeField] 
    private float _speed = 2;

    [SerializeField]
    private float _aimSpeed = 30;



    //--Private Variables
    private float _movementInput;
    private float _aimInput;



    //--Component References
    [SerializeField] 
    private Rigidbody2D _rigid2d;

    [SerializeField]
    private Transform _turretTransform;

    [SerializeField]
    private Trajectory _trajectory;

    [SerializeField]
    private Bullet_1 _bullet;


    private void Start()
    {
     
    }
    void Update()
    {
        _movementInput = Input.GetAxis("Horizontal") * _speed;

        _aimInput = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    {
        Movment();
        Aim();
        
        _trajectory.UpdateDots(_bullet.pose, _bullet.force);
    }

    void Movment()
    {
        if (_movementInput == 0f)
        { 
            _rigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            _rigid2d.velocity = new Vector2(_movementInput, _rigid2d.velocity.y);

            _rigid2d.constraints = RigidbodyConstraints2D.None;
        }

        StableAngleTank();
    }

    void Aim()
    {
        Vector3 aimAngle = new Vector3(0, 0, _aimInput);

        _turretTransform.Rotate(aimAngle, Time.deltaTime * _aimSpeed);

        StableAngleTurret();
        _trajectory.Show();
    }

    void StableAngleTank()
    {
        Vector3 currentRotation = transform.eulerAngles;
        if (currentRotation.z > 180)
        {
            currentRotation.z = currentRotation.z - 360; 
        }
        currentRotation.z = Mathf.Clamp(currentRotation.z, -5f, 45f);
        transform.eulerAngles = currentRotation;
    }

    void StableAngleTurret()
    {
        Vector3 currentRotation = _turretTransform.localRotation.eulerAngles;
        if (currentRotation.z > 180)
        {
            currentRotation.z = currentRotation.z - 360;
        }
        currentRotation.z = Mathf.Clamp(currentRotation.z, 0, 50);
        _turretTransform.localRotation = Quaternion.Euler(currentRotation);
    }

}
