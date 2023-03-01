using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coroutine
{
    public static class CoroutineSchedule
    {
        private const int UpdateInterval = 30;

        private static readonly List<Coroutine> _coroutines = new List<Coroutine>();
        private static readonly List<Coroutine> _waitAdds = new List<Coroutine>();
        private static readonly List<Coroutine> _waitRemoves = new List<Coroutine>();

        private static bool _isStarted = false;

        public static Coroutine StartCoroutine(this Control control, IEnumerator enumerator)
        {
            control.HandleDestroyed += HandleDestroyed;
            var coroutine = new Coroutine { Control = control, Enumerator = enumerator };
            _coroutines.Add(coroutine);
            if (!_isStarted) Updater();
            return coroutine;
        }
        public static void StopCoroutine(this Control control, Coroutine coroutine)
        {
            if (coroutine != null)
                _waitRemoves.Add(coroutine);
        }
        private static async void Updater()
        {
            _isStarted = true;
            while (_coroutines.Count > 0)
            {
                foreach (var item in _coroutines)
                {
                    var enumerator = item.Enumerator;
                    if (enumerator == null) continue;
                    if (enumerator.Current == null)
                        enumerator.MoveNext();

                    if (enumerator.Current is IYieldInstruction keepWait && keepWait.IsMoveNext())
                    {
                        var isEnd = !enumerator.MoveNext();
                        if (!isEnd) continue;
                        _waitRemoves.Add(item);
                        if (item.Parent == null) continue;
                        item.Parent.IsWait = false;
                        var isEndParent = !item.Parent.Enumerator.MoveNext();
                        if (isEndParent)
                        {
                            _waitRemoves.Add(item.Parent);
                        }
                    }
                    else
                    {
                        if (item.IsWait) continue;
                        if (!(enumerator.Current is IEnumerator newEnumerator)) continue;
                        item.IsWait = true;
                        _waitAdds.Add(item);
                    }
                }

                foreach (var item in _waitRemoves)
                {
                    item.Control.HandleDestroyed -= HandleDestroyed;
                    _coroutines.Remove(item);
                }

                _waitRemoves.Clear();

                foreach (var item in _waitAdds)
                {
                    YieldCoroutine(item);
                }

                _waitAdds.Clear();
                await Task.Delay(UpdateInterval);
            }

            _isStarted = false;
        }

        private static void HandleDestroyed(object sender, EventArgs e)
        {
            _waitRemoves.Add(_coroutines.Find(co => co.Control.Equals(sender)));
        }

        private static void YieldCoroutine(Coroutine coroutineInfo)
        {
            coroutineInfo.Control.HandleDestroyed += HandleDestroyed;
            _coroutines.Add(new Coroutine
            {
                Parent = coroutineInfo, Control = coroutineInfo.Control,
                Enumerator = coroutineInfo.Enumerator.Current as IEnumerator
            });
        }
    }
}