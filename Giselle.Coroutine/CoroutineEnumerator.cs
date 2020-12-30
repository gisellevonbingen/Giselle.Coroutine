using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public class CoroutineEnumerator : Coroutine
    {
        public IEnumerator Routine { get; private set; }

        public CoroutineEnumerator(IEnumerator routine)
        {
            this.Routine = routine;
        }

        protected override bool OnMoveNext(double delta)
        {
            return this.MoveNext(delta, this.Routine);
        }

        protected bool MoveNext(double delta, IEnumerator routine)
        {
            if (this.Started == true)
            {
                var current = routine.Current;
                var hasSubroutine = this.MoveSubroutine(delta, current);

                if (hasSubroutine == true)
                {
                    return true;
                }

            }

            var result = routine.MoveNext();
            return result;
        }

        protected bool MoveSubroutine(double delta, object current)
        {
            if (current is IEnumerator)
            {
                if (this.MoveNext(delta, (IEnumerator)current) == true)
                {
                    return true;
                }

            }
            else if (current is IRoutine)
            {
                if (((IRoutine)current).MoveNext(delta) == true)
                {
                    return true;
                }

            }
            else if (current is ICoroutine)
            {
                if (((ICoroutine)current).Complete == false)
                {
                    return true;
                }

            }

            return false;
        }

    }

}
