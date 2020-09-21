using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmSystem : MonoBehaviour
{
    public AudioSource alarmSound;

    private void Start()
    {
        alarmSound = GetComponent<AudioSource>();
    }

    private IEnumerator ChangeVolume(int startValue, int endValue, TurnOnOffAlarm turnOnOffAlarm)
    {
        float time = 0;

        if (turnOnOffAlarm == TurnOnOffAlarm.Play)
            alarmSound.Play();

        while (time < 1)
        {
            float volumeValue = Mathf.Lerp(startValue, endValue, time / 1);
            time += Time.deltaTime;
            alarmSound.volume = volumeValue;
            yield return null;
        }

        if (turnOnOffAlarm == TurnOnOffAlarm.Stop)
            alarmSound.Stop();
    }

    private enum TurnOnOffAlarm
    {
        Play = 0,
        Stop = 1
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ChangeVolume(0, 1, TurnOnOffAlarm.Play));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(ChangeVolume(1, 0, TurnOnOffAlarm.Stop));
    }
}
