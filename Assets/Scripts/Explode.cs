using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explode : MonoBehaviour
{
    [SerializeField]
    protected GameObject _explosion;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            hit.Damage();
        }

        if(_explosion != null)
        {
            GameObject explode = Instantiate(_explosion, transform.position, Quaternion.identity);

            Destroy(explode, 0.41f);
        }
        
        Destroy(this.gameObject);
    }
}