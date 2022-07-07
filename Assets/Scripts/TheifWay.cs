using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheifWay : MonoBehaviour
{
    [SerializeField] private Transform _theifWay;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPoint;

    private void Start()
    {
        _points = new Transform[_theifWay.childCount];

        for(int i = 0; i < _theifWay.childCount; i++)
        {
            _points[i] = _theifWay.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        Debug.Log(transform.position);

        transform.position =  Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if(transform.position == target.position)
        {
            _currentPoint++;

            if(_currentPoint == _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
