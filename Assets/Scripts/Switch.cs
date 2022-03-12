using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private Sprite _spriteOn;
    [SerializeField]
    private Sprite _spriteOff;
    [SerializeField]
    private bool _isSwitchActiv = true;
    [SerializeField]
    private Color _redColor;
    [SerializeField]
    private Color _greenColor;


    [SerializeField]
    private string[] _tagOfCollidedGameObject;
    [SerializeField]
    private GameObject[] _switchObjects;

    private SpriteRenderer _switchSR;
    private Mine[] _switchObjectsMine;


    private void Start()
    {
        _switchSR = gameObject.GetComponentInChildren<SpriteRenderer>();

        int switchObjectLength = _switchObjects.Length;
        _switchObjectsMine = new Mine[switchObjectLength];

        for (int i = 0; i < _switchObjects.Length; i++)
        {
            _switchObjectsMine[i] = _switchObjects[i].GetComponent<Mine>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int u = 0; u < _tagOfCollidedGameObject.Length; u++)
        {
            if (other.tag == _tagOfCollidedGameObject[u])
            {
                if (_isSwitchActiv == true)
                {
                    TurnSwich(_spriteOff, false);
                    _switchSR.color = _redColor;

                    for (int e = 0; e < _switchObjects.Length; e++)
                    {
                        if (_switchObjectsMine[e] != null)
                        _switchObjectsMine[e].SetDeactive();
                    }
                }
            }
        }
    }
    private void TurnSwich(Sprite sprite, bool condition)
    {
        _switchSR.sprite = sprite;
        _isSwitchActiv = condition;   
    }
}
