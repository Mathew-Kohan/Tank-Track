using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected int _powerUpID;

    [SerializeField]
    protected int _power;

    [SerializeField]
    protected string _description;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.PowerUP(_powerUpID, _power);
            }

            Destroy(this.gameObject);
        }
    }
}
