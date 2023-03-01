using System;

namespace Coroutine
{
    public sealed class WaitForSeconds : IYieldInstruction
    {
        private float _seconds;
        private DateTime _after;

        public WaitForSeconds(float seconds)
        {
            _seconds = seconds;
            _after = DateTime.Now.AddSeconds(_seconds);
        }

        public bool IsMoveNext()
        {
            return _after <= DateTime.Now;
        }
    }
}