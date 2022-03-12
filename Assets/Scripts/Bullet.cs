using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Explode
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            base.OnTriggerEnter2D(other);
        }
    }
}
