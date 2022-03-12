using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] 
    private int _dotsNumber;
    [SerializeField] 
    private GameObject _dotsParent;
    [SerializeField] 
    private GameObject _dotPrefab;
    [SerializeField] 
    private float _dotSpacing;

    [SerializeField] [Range(0.01f, 0.3f)] 
    private float _dotMinScale;
    [SerializeField] [Range(0.3f, 1f)] 
    private float _dotMaxScale;

    private Transform[] _dotList;
    private Vector2 _pos; //dot position
    private float _timeStamp;

    private void Start()
    {
        Hide(); //Hide Trajectory in the start

        PrepareDots();
    }

    private void PrepareDots()
    {
        _dotList = new Transform[_dotsNumber];
        _dotPrefab.transform.localScale = Vector3.one * _dotMaxScale;

        float scale = _dotMaxScale;
        float scaleFactor = scale / _dotsNumber;
        for(int i = 0; i < _dotsNumber; i++)
        {
            _dotList[i] = Instantiate(_dotPrefab, null).transform;
            _dotList[i].parent = _dotsParent.transform;

            _dotList[i].localScale = Vector3.one * scale;

            if(scale > _dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots(Vector3 bulletPos, Vector2 forceApplied)
    {
        _timeStamp = _dotSpacing;
        for (int i = 0; i < _dotsNumber; i++)
        {
            _pos.x = (bulletPos.x + forceApplied.x * _timeStamp);
            _pos.y = (bulletPos.y + forceApplied.y * _timeStamp) - (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;

            _dotList[i].position = _pos;
            _timeStamp += _dotSpacing;
        }
    }

    public void Show()
    {
        _dotsParent.SetActive(true);
    }

    public void Hide()
    {
        _dotsParent.SetActive(false);
    }
}
