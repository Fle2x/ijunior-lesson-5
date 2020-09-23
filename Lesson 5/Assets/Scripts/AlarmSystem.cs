﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource alarm;

    private void Start()
    {
        alarm = GetComponent<AudioSource>();
    }

    private IEnumerator ChangeVolume(int startValue, int endValue, AlarmStatus alarmStatus)
    {
        float time = 0;

        if (alarmStatus == AlarmStatus.On)
            alarm.Play();

        while (time < 1)
        {
            float volumeValue = Mathf.Lerp(startValue, endValue, time / 1);
            time += Time.deltaTime;
            alarm.volume = volumeValue;
            yield return null;
        }

        if (alarmStatus == AlarmStatus.Off)
            alarm.Stop();
    }

    private enum AlarmStatus
    {
        On = 0,
        Off = 1
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ChangeVolume(0, 1, AlarmStatus.On));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(ChangeVolume(1, 0, AlarmStatus.Off));
    }
}
