namespace Coroutine
{
    public sealed class WaitWhile : IYieldInstruction
    {
        public delegate bool Condition();

        private Condition _condition;

        public WaitWhile(Condition condition)
        {
            _condition = condition;
        }

        public bool IsMoveNext()
        {
            return !_condition.Invoke();
        }
    }
}