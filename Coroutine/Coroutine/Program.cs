// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Timers;
using Coroutine.Coroutine;
using Coroutine.Instruction;


class Program
{
    static readonly System.Timers.Timer _timer = new System.Timers.Timer(10);

    static void Main(string[] args)
    {
        _timer.Elapsed += TimerElapsed;
        _timer.Start();
        Console.WriteLine("Hello, World!");
        CoroutineExample();
        while (Console.ReadKey().Key != ConsoleKey.Escape)
        {
        }
    }

    static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        CoroutineMgr.Instance.OnUpdate();
    }


    static async void CoroutineExample()
    {
        Console.WriteLine("使用协程模块的StartCoroutine方法开始运行协程：\n");
        await CoroutineMgr.Instance.StartCoroutine(WaitExample());
        Console.WriteLine("协程示例结束");
    }

    /// <summary>
    /// 协程开始
    /// </summary>
    /// <returns></returns>
    static IEnumerator StartExample()
    {
        Console.WriteLine("开始一个协程");
        yield return new WaitForFrame();
        Console.WriteLine("等待一帧");
    }

    static IEnumerator WaitExample()
    {
        Console.WriteLine("协程WaitExample开始");

        yield return new WaitForFrame();
        Console.WriteLine($"一帧过去了 = {DateTime.Now.ToString("HH:mm:ss zz")}");

        yield return new WaitForFrames(20);
        Console.WriteLine($"20帧过去了={DateTime.Now.ToString("HH:mm:ss zz")}");

        yield return new WaitForSeconds(3);
        Console.WriteLine($"等待3秒时间过去了={DateTime.Now.ToString("HH:mm:ss zz")}");
    }
}