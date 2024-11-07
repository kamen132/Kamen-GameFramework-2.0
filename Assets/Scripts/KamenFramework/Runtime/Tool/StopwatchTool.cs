using System.Diagnostics;
using KamenFramework;

public class StopwatchTool
{
    private Stopwatch stopwatch = new Stopwatch();
    private string mDesc;
    private int mIndex;
    public StopwatchTool(string desc)
    {
        mDesc = desc;
    }
    public void Start()
    {
        stopwatch.Start();
    }

    public void Stop()
    {
        stopwatch.Stop();
    }
    
    public void LogInfo()
    {
        stopwatch.Stop();
        KLogger.LogError($"{mDesc}-{mIndex}-耗时 " + (float) stopwatch.Elapsed.TotalSeconds + "秒");
        Restart();
    }

    public void Restart()
    {
        stopwatch.Restart();
        mIndex++;
    }
}