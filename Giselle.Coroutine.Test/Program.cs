using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giselle.Coroutine.Test
{
    public class Program
    {
        public static CoroutineManager Manager = new CoroutineManager();
        public static Stopwatch Stopwatch = new Stopwatch();
        public static double LastMillis = 0.0D;

        public static void Main()
        {
            Manager.Start(Test());

            var timer = new Timer(OnTimerTick);
            timer.Change(new TimeSpan(), TimeSpan.FromMilliseconds(1.0D / Stopwatch.Frequency * 100000000));

            Console.ReadLine();
        }

        private static IEnumerator Test()
        {
            Console.WriteLine("A");

            var r = Manager.Start(Test2());
            yield return r;

            Console.WriteLine("Z");
        }

        private static IEnumerator Test2()
        {
            yield return new WaitDuration(1000);
            Console.WriteLine("B");
            yield return new WaitDuration(1000);
            Console.WriteLine("C");
            yield return new WaitDuration(1000);
            Console.WriteLine("D");
            yield return new WaitDuration(1000);
            Console.WriteLine("E");
        }

        private static void OnTimerTick(object sender)
        {
            var sw = Stopwatch;
            var delta = 0.0D;

            if (sw.IsRunning == false)
            {
                sw.Restart();
            }
            else
            {
                var millis = sw.Elapsed.TotalMilliseconds;
                delta = millis - LastMillis;
                LastMillis = millis;
            }

            Manager.Update(delta);
        }

    }

}

