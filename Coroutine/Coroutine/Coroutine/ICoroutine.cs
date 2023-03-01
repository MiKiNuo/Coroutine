using Coroutine.Interfaces;

namespace Coroutine.Coroutine;

public interface ICoroutine : IAwaitable<CoroutineAwaiter>
{
    /// <summary>
    /// 目前状态
    /// </summary>
    CoroutineState State { get; }

    /// <summary>
    /// 手动结束
    /// </summary>
    void Compelete();

    /// <summary>
    /// 挂起
    /// </summary>
    void Pause();

    /// <summary>
    /// 恢复运行
    /// </summary>
    void Resume();
}