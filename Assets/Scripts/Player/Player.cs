using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int coins;

    //--Private Variables Exposed to the Inspector
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private HealthBar _healthBar;

    [SerializeField] 
    private float _speed = 1.8f;
    [SerializeField]
    private float _aimSpeed = 30;



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



    private Rigidbody2D _rigid2d;
    private Shooting _shooting;

    private bool _isAimUp = false;
    private bool _isAimDown = false;

    private void Start()
    {
        _health = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        Health = _health;
        _healthBar.SetHealth(_health);

        _rigid2d = GetComponent<Rigidbody2D>();
        _shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        UIManager.Instance.CoinCount(coins);

        if (Input.GetKeyDown(KeyCode.Space))
            Fire();

    }

    private void FixedUpdate()
    {
        Move();
        Aim();           

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
            _shooting.isBulletActiv = true;
            _rigid2d.AddTorque(0.6f, ForceMode2D.Impulse);
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
        _healthBar.SetHealth(_health);

        GameObject explode = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explode, 2f);

        if (_health < 1)
        {
            Destroy(gameObject, 0.2f);

            UIManager.Instance.GameOverMenu();
        }
    }

    public void PowerUP(int powerID, int power)
    {
        switch (powerID)
        {
            case 0:

                break;
            case 1:
                _health = _maxHealth;
                _healthBar.SetHealth(_health);
                break;
            case 2:
                _shooting.bulletCount += power;
                UIManager.Instance.BulletCount(_shooting.bulletCount);
                break;
        }
    }
}
