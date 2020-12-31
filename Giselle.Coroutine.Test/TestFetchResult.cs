using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Giselle.Coroutine.Test
{
    public static class TestFetchResult
    {
        public static IEnumerator Test()
        {
            Console.WriteLine("A");

            var r = Program.Manager.Start(Test2());
            yield return r;

            foreach (var str in r.Results)
            {
                Console.WriteLine(str);
                yield return new WaitDuration(500);
            }

            Console.WriteLine("Z");
        }

        public static IEnumerator<CoroutineAction<string>> Test2()
        {
            yield return new WaitDuration(1000);
            yield return "B";
            yield return new WaitDuration(1000);
            yield return "C";
            yield return new WaitDuration(1000);
            yield return "D";
            yield return new WaitDuration(1000);
            yield return "E";
            yield return new WaitDuration(1000);
            yield return Test3().ToAction<string>();
        }

        public static IEnumerator<CoroutineAction<string>> Test3()
        {
            yield return "F";
        }

    }

}
