using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class TimerAdapter : Timer, ITimer
{
    public void StartTimeout(int durationInSeconds)
    {
        if (Enabled)
            return;

        Enabled = true;
        Elapsed += TimeoutElapsed;
        Interval = TimeSpan.FromSeconds(durationInSeconds).TotalMilliseconds;
    }

    private void TimeoutElapsed(object source, ElapsedEventArgs e)
    {
        Stop();
    }

}
