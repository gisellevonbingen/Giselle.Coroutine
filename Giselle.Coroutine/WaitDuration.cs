using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public class WaitDuration : IRoutine
    {
        public long Millis { get; private set; }
        public double Elapsed { get; private set; }

        public WaitDuration(long millis)
        {
            this.Millis = millis;
            this.Elapsed = 0.0D;
        }

        public bool MoveNext(double delta)
        {
            this.Elapsed += delta;
            return this.Elapsed < this.Millis;
        }

    }

}
