namespace Coroutine.Instruction;

/// <summary>
/// 等待秒
/// </summary>
public class WaitForSeconds : WaitForTimeSpan
{
    public WaitForSeconds(double seconds) : base(TimeSpan.FromSeconds(seconds))
    {
    }
}