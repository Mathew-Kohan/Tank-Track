using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _fieldofImpact;
    [SerializeField] private float _force;
    [SerializeField] private LayerMask _layertoHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            explosion();
    }
    void explosion()
    {
        Collider2D[] objectsImpact = Physics2D.OverlapCircleAll(transform.position, _fieldofImpact, _layertoHit);

        foreach(Collider2D obj in objectsImpact)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * _force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fieldofImpact);
    }
}
