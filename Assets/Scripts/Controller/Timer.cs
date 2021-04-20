using System;
using UnityEngine;

public class Timer 
{
    private float _startTime;
    private float _currentTime;
    private float _endTime;
    private bool _isSet = false;

    const float SEC_TO_MILLISEC = 1000.0f;

    public void SetTimer (int timeInSeconds)
    {
        _startTime = Time.time;
        _currentTime = Time.time;
        _endTime = _startTime + timeInSeconds * SEC_TO_MILLISEC;

        _isSet = true;
    }

    public bool ResetTimer()
    {
        _isSet = false;
        return true;
    }

    public void Update()
    {
        if(_isSet)
        {
            if(_currentTime >= _endTime)
            {
                ResetTimer();
                return;
            }
            _currentTime = Time.time;
        }
    }
}
