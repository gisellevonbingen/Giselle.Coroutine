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
        public static CoroutineManager Manager { get; private set; }
        public static Stopwatch Stopwatch { get; private set; }
        public static double LastMillis { get; private set; }

        public static void Main()
        {
            Manager = new CoroutineManager();
            Stopwatch = new Stopwatch();
            LastMillis = 0.0D;

            var timer = new Timer(OnTimerTick);
            timer.Change(new TimeSpan(), TimeSpan.FromMilliseconds(1.0D / Stopwatch.Frequency * 100000000));

            Manager.Start(TestSimple.Test());

            Console.ReadLine();
        }

        private static void OnTimerTick(object sender)
        {
            var delta = 0.0D;

            if (Stopwatch.IsRunning == false)
            {
                Stopwatch.Restart();
            }
            else
            {
                var millis = Stopwatch.Elapsed.TotalMilliseconds;
                delta = millis - LastMillis;
                LastMillis = millis;
            }

            Manager.Update(delta);
        }

    }

}

