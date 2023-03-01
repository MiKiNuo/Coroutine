using System.Collections;

namespace Coroutine.Instruction;

/// <summary>
/// 所有等待类的基类
/// </summary>
public abstract class YieldInstruction
{
    /// <summary>
    /// 是否结束
    /// </summary>
    public virtual bool isDone => IsCompelete();

    /// <summary>
    /// 是否结束
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsCompelete();

    public IEnumerator AsEnumerator()
    {
        while (!isDone)
        {
            yield return false;
        }

        yield return true;
    }
}