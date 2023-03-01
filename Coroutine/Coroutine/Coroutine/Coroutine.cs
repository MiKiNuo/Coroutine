using System.Collections;
using Coroutine.Instruction;

namespace Coroutine.Coroutine;

public sealed class Coroutine : YieldInstruction, ICoroutine,IDisposable
{
    private IEnumerator _routine;
    public event Action OnCompelete;

    public CoroutineState State { get; private set; }

    public Coroutine(IEnumerator routine)
    {
        _routine = routine;
    }

    public void Compelete()
    {

        
        if (OnCompelete != null)
            OnCompelete();

        OnCompelete = null;
        _routine = null;
        State = CoroutineState.Rest;
        Dispose();
    }

    public CoroutineAwaiter GetAwaiter()
    {
        return new CoroutineAwaiter(this);
    }

    protected override bool IsCompelete()
    {
        if (State != CoroutineState.Working)
            return false;

        if (!_routine.MoveNext())
            return true;

        if (_routine.Current != null)
        {
            var instruction = _routine.Current as YieldInstruction;
            var moveNext = true;
            if (instruction!= null)
                moveNext = instruction.isDone;
            if (!moveNext)
            {
                return true;
            }
            else
            {
                return _routine.MoveNext();
            }

        }

        return false;
    }


    public void Pause()
    {
        State = CoroutineState.Yied;
    }

    public void Resume()
    {
        State = CoroutineState.Working;
    }

    public void Dispose()
    {
        
    }
}