using Coroutine.Interfaces;

namespace Coroutine.Coroutine;

public struct CoroutineAwaiter : IAwaiter, ICriticalAwaiter
{
    private Coroutine _cor;
    private Queue<Action> _calls;

    public CoroutineAwaiter(ICoroutine cor)
    {
        this._cor = cor as Coroutine;
        _calls = new Queue<Action>();
        this._cor.OnCompelete += TaskCompleted;
    }

    private void TaskCompleted()
    {
        while (_calls.Count != 0)
        {
            _calls.Dequeue()?.Invoke();
        }
    }

    public bool IsCompleted => _cor.isDone;

    public void GetResult()
    {
        if (!IsCompleted)
            throw new Exception("The task is not finished yet");
    }

    public void OnCompleted(Action continuation)
    {
        UnsafeOnCompleted(continuation);
    }

    public void UnsafeOnCompleted(Action continuation)
    { 
        if (continuation == null)
            throw new ArgumentNullException(nameof(continuation));
        _calls.Enqueue(continuation);
    }
}