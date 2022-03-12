using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int coins;

    //--Private Variables Exposed to the Inspector
    [SerializeField] 
    private float _speed = 1.5f;
    [SerializeField]
    private float _aimSpeed = 30;

    [SerializeField]
    private int _health = 1;


    //--Private Variables
    private float _movementInput;
    private float _touchInput;

    //--Component References
    [SerializeField]
    private Transform _turretTransform;

    [SerializeField]
    private Trajectory _trajectory;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _CMvcam;

    private Rigidbody2D _rigid2d;
    private Shooting _shooting;

    private bool _isAimUp = false;
    private bool _isAimDown = false;

    private void Start()
    {
        Health = _health;

        _rigid2d = GetComponent<Rigidbody2D>();
        _shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        UIManager.Instance.CoinCount(coins); 
    }

    private void FixedUpdate()
    {
        Move();
        Aim();

        if(Input.GetKey(KeyCode.Space))
            Fire();

        _trajectory.UpdateDots(_shooting.pose, _shooting.force);     
    }

    private void Move()
    {
        _movementInput = (Input.GetAxis("Horizontal") + _touchInput) * _speed;

        if (_movementInput == 0f)
        {
            _rigid2d.velocity = Vector2.zero;
            StartCoroutine(FreezeConstrain());
        }
        else
        {
            _rigid2d.velocity = new Vector2(_movementInput, _rigid2d.velocity.y);
            _rigid2d.constraints = RigidbodyConstraints2D.None;
        }

        StableAngleTank();
    }

    IEnumerator FreezeConstrain()
    {
        yield return new WaitForSeconds(0.3f);
        _rigid2d.constraints = RigidbodyConstraints2D.FreezePositionX;    
    }

    public void MoveButton(int i)
    {
        _touchInput = i;
    }

    private void Aim()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || _isAimUp == true)
        {
            AimMove(1);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || _isAimDown == true)
        {
            AimMove(-1);
        }
        else
        {
            _trajectory.Hide();
        }

        StableAngleTurret();
    }

    private void AimMove(int i)
    {
        Vector3 aimAngle = new Vector3(0, 0, i);

        _turretTransform.Rotate(aimAngle, Time.deltaTime * _aimSpeed);

        _trajectory.Show();
    }

    public void AimUpButton(bool isMove)
    {
        _isAimUp = isMove;
    }

    public void AimDownButton(bool isMove)
    {
        _isAimDown = isMove;
    }

    public void Fire()
    {
        if (_shooting.bulletCount >= 1)
        {
            _shooting.Shoot();
            _rigid2d.AddTorque(0.2f, ForceMode2D.Impulse);
        }
    }

    private void StableAngleTank()
    {
        Vector3 currentRotation = transform.eulerAngles;
        if (currentRotation.z > 180)
        {
            currentRotation.z -= 360; 
        }
        currentRotation.z = Mathf.Clamp(currentRotation.z, -40f, 45f);
        transform.eulerAngles = currentRotation;
    }

    private void StableAngleTurret()
    {
        Vector3 currentRotation = _turretTransform.localRotation.eulerAngles;
        if (currentRotation.z > 180)
        {
            currentRotation.z -= 360;
        }
        currentRotation.z = Mathf.Clamp(currentRotation.z, 0, 50);
        _turretTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void Damage()
    {
        _health--;

        GameObject explode = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explode, 2f);

        if (_health < 1)
        {
            _CMvcam.SetActive(false);

            Destroy(gameObject, 0.2f);

            UIManager.Instance.GameOverMenu();
        }
    }
}
