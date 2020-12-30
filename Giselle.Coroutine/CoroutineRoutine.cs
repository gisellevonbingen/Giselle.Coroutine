using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public class CoroutineRoutine : Coroutine
    {
        public IRoutine Routine { get; private set; }

        public CoroutineRoutine(IRoutine routine)
        {
            this.Routine = routine;
        }

        protected override bool OnMoveNext(double delta)
        {
            return this.Routine.MoveNext(delta);
        }

    }

}
