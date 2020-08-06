using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _minutes;
    private float _seconds;
    private float _timer;

    private int _timeScale = 2;
    private float _previousTime = 0.0f;

    private bool _isExpired;


    public Timer()
    {
        _timer = 180.0f;
        _seconds = 0.0f;
        _minutes = (int) (_timer / 60.0f);

        _isExpired = false;
    }

    public Timer(float value)
    {
        _timer = value;
        _seconds = 0.0f;
        _minutes = (int) (_timer / 60.0f);

        _isExpired = false;
    }
    public void ShowTimer(float duration = 1.0f)
    {
        if (Time.time - _previousTime > duration)
        {
            _seconds = (int) (--_timer  % 60);
            _minutes = (int) (_timer / 60.0f);
            EventManager.TriggerEvent(new OnShowTimerEvent {FormatedTime = GetFormattedTimer()});
            _previousTime = Time.time;
            
            if (_seconds < 1.0f && _minutes  < 1.0f)
            {
                _isExpired = true;
                EventManager.TriggerEvent<OnTimerExpiredEvent>();
            }
        }

    }

    public void SetTimer(float value)
    {
        _timer = value;
    }

    public void Reset()
    {
        SetTimer(180.0f);
    }

    public bool IsExpired()
    {
        return _isExpired;
    }

    public string GetFormattedTimer()
    {
        return String.Format("{0:00} : {1:00}", _minutes, _seconds);
    }
}