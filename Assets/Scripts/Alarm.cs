using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioAlarm;
    private bool _isAlarmWork = false;
    private int _targetVolumeUp = 1;
    private int _targetVolumeDown = 0;
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
                StartCoroutine(ChangeAlarmVolume(_targetVolumeUp));              
            }
            else
            {
                _isAlarmWork = false;
                _animator.SetTrigger(AlarmOff);
                StartCoroutine(ChangeAlarmVolume(_targetVolumeDown));
            }            
        }
    }

    private IEnumerator ChangeAlarmVolume(int targetVolume)
    {
        bool isVolumeChanging = true;

        while(isVolumeChanging)
        {
            _audioAlarm.volume = Mathf.MoveTowards(_audioAlarm.volume, targetVolume, Time.deltaTime);

            if(_audioAlarm.volume == targetVolume)
            {
                isVolumeChanging = false;
            }

            yield return null;
        }
    }
}
