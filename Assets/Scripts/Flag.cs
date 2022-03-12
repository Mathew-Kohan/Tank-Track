using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Animator anim = GetComponentInChildren<Animator>();
            anim.SetBool("isWin", true);

            UIManager.Instance.WinMenu();
        }
    }
}
