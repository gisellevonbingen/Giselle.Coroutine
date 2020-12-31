using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine.Test
{
    public static class TestSimple
    {
        public static IEnumerator Test()
        {
            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine("=== TEST " + i + " ===");
                yield return Test2(i + 1);
                yield return new WaitDuration(500);
            }

        }

        public static IEnumerator Test2(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new WaitDuration(500);
                Console.WriteLine("TEST2 : " + i);
            }

        }

    }

}
