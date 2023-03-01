namespace Coroutine
{
    public sealed class WaitUntil : IYieldInstruction
    {
        public delegate bool Condition();

        private Condition _condition;

        public WaitUntil(Condition condition)
        {
            _condition = condition;
        }

        public bool IsMoveNext()
        {
            return _condition.Invoke();
        }
    }
}