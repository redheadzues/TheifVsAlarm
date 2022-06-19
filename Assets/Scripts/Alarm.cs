using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioAlarm;
    private int _volumeSteps = 300;
    private bool _isAlarmWork = false;
    private float _maxVolumeValue = 1f;
    private const string AlarmOn = "AlarmOn";
    private const string AlarmOff = "AlarmOff";

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
                _animator.SetTrigger(AlarmOn);
                var das = StartCoroutine(ChangeAlarmVolume(_volumeSteps, true));              
            }
            else
            {
                _isAlarmWork = false;
                _animator.SetTrigger(AlarmOff);
                StartCoroutine(ChangeAlarmVolume(_volumeSteps, false));
            }            
        }
    }

    private IEnumerator ChangeAlarmVolume(int volumeSteps, bool isIncrease)
    {        
        for(int i = 0; i < volumeSteps; i++)
        {
            if(isIncrease == true)
            {
                _audioAlarm.volume += _maxVolumeValue / volumeSteps;
            }
            else
            {
                _audioAlarm.volume -= _maxVolumeValue / volumeSteps;
            }
                
            yield return null;
        }
    }
}
