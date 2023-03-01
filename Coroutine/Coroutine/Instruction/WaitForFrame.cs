namespace Coroutine.Instruction;

/// <summary>
/// 等一帧
/// </summary>
public sealed class WaitForFrame:WaitForFrames
{
    /// <summary>
    /// Ctor
    /// </summary>
    public WaitForFrame() : base(1)
    {
    }
}