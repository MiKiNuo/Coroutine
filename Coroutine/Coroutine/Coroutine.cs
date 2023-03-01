using System.Collections;
using System.Windows.Forms;

namespace Coroutine
{
    public class Coroutine
    {
        public Coroutine Parent { set; get; }
        public Control Control { set; get; }
        public IEnumerator Enumerator { set; get; }
        public bool IsWait { set; get; }
    }
}