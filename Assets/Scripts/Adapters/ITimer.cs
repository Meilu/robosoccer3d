using System.Timers;

public interface ITimer
{
    void Start();
    void Stop();
    double Interval { get; set; }
    bool Enabled { get; set; }
    bool AutoReset { get; set; }
    event ElapsedEventHandler Elapsed;

    void StartTimeout(int durationInSeconds);
}