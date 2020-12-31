using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public abstract class Routine
    {
        public abstract bool MoveNext(double delta);
    }

}
