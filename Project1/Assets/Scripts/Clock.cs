using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public Transform seconds_pivot, minutes_pivot, hours_pivot;
    const float seconds_to_degrees = -6;
    const float minutes_to_degrees = -6;
    const float hours_to_degrees = -30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        seconds_pivot.localRotation =
            Quaternion.Euler(0f, 0f, seconds_to_degrees * (float)time.TotalSeconds);
        minutes_pivot.localRotation =
            Quaternion.Euler(0f, 0f, minutes_to_degrees * (float)time.TotalMinutes);
        hours_pivot.localRotation =
            Quaternion.Euler(0f, 0f, hours_to_degrees * (float)time.TotalHours);
    }
}
