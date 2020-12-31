using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public static class CoroutineExtensions
    {
        public static CoroutineAction ToAction(this IEnumerator enumerator)
        {
            return CoroutineAction.Enumerator(enumerator);
        }

        public static CoroutineAction<T> ToAction<T>(this IEnumerator enumerator)
        {
            return CoroutineAction<T>.Enumerator(enumerator);
        }

        public static CoroutineAction<T> ToAction<T>(this IEnumerator<CoroutineAction<T>> enumerator)
        {
            return CoroutineAction<T>.Enumerator(enumerator);
        }

    }

}
