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
                var das = StartCoroutine(ChangeAlarmVolume(1));              
            }
            else
            {
                _isAlarmWork = false;
                _animator.SetTrigger(AlarmOff);
                StartCoroutine(ChangeAlarmVolume(0));
            }            
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
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
