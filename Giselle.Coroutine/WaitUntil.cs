using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public class WaitUntil : Routine
    {
        public Func<double, bool> Func { get; private set; }

        public WaitUntil(Func<double, bool> func)
        {
            this.Func = func;
        }

        public override bool MoveNext(double delta)
        {
            var result = this.Func(delta);
            return result == false;
        }

    }

}
