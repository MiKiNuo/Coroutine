namespace Coroutine.Instruction;

/// <summary>
/// 等待帧数
/// </summary>
public class WaitForFrames : YieldInstruction
{
    public WaitForFrames(int count)
    {
        _curCount = 0;
        this._count = count;
    }

    private int _curCount;
    private int _count { get; }
    
    protected override bool IsCompelete()
    {
        _curCount++;
        return _curCount >= _count;
    }
}