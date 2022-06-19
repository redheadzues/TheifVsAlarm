using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeStep;

    private Animator _animator;
    private AudioSource _audioAlarm;
    private bool _isAlarmWork = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioAlarm = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Theif>(out Theif theif))
        {
            if(_isAlarmWork == false)
            {
                _isAlarmWork = true;
                _animator.SetTrigger("AlarmOn");
            }
            else
            {
                _isAlarmWork = false;
                _animator.SetTrigger("AlarmOff");
            }            
        }
    }

    private void Update()
    {
        if(_isAlarmWork == true)
        {
            _audioAlarm.volume += _volumeStep;
        }
        else
        {
            _audioAlarm.volume -= _volumeStep;
        }
    }
}
