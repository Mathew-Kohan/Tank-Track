using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Explode
{
    [SerializeField]
    private Color _lowTransparent;

    private SpriteRenderer _mineSR;
    private CapsuleCollider2D _mineCC2D;

    private void Start()
    {
        _mineSR = GetComponent<SpriteRenderer>();
        _mineCC2D = GetComponent<CapsuleCollider2D>();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public void SetDeactive()
    {
        _mineSR.color = _lowTransparent;
        _mineCC2D.enabled = false;
    }
}
