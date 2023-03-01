using System.Collections;
using System.Timers;

namespace Coroutine.Coroutine;

public sealed class CoroutineMgr
{
    private static readonly Lazy<CoroutineMgr> _instance = new(() => new CoroutineMgr());


    public static CoroutineMgr Instance => _instance.Value;

    private Lazy<List<ICoroutine>> _cors = new(() => new List<ICoroutine>());

    private CoroutineMgr()
    {
    }


    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        OnUpdate();
    }

    /// <summary>
    /// 开启一个协程
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public ICoroutine StartCoroutine(IEnumerator routine)
    {
        var coroutine = new Coroutine(routine);
        _cors.Value.Add(coroutine);
        ResumeCoroutine(coroutine);
        return coroutine;
    }

    public ICoroutine CreateCoroutine(IEnumerator routine)
    {
        var coroutine = new Coroutine(routine);
        PauseCoroutine(coroutine);
        return coroutine;
    }

    /// <summary>
    /// 挂起携程
    /// </summary>
    /// <param name="coroutine"></param>
    public void PauseCoroutine(ICoroutine coroutine)
    {
        coroutine.Pause();
    }

    /// <summary>
    /// 恢复运行
    /// </summary>
    /// <param name="coroutine"></param>
    public void ResumeCoroutine(ICoroutine coroutine)
    {
        coroutine.Resume();
    }

    /// <summary>
    /// 关闭一个携程
    /// </summary>
    /// <param name="coroutine"></param>
    public void StopCoroutine(ICoroutine coroutine)
    {
        coroutine.Compelete();
    }

    public void OnUpdate()
    {
        var count = _cors.Value.Count;
        for (var i = 0; i < count; i++)
        {
            var cor = _cors.Value[i];
            if (cor.State != CoroutineState.Working) continue;
            if ((cor as Coroutine).isDone)
                cor.Compelete();
        }
    }
}