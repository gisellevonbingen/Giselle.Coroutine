using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public interface ICoroutine
    {
        bool Started { get; }
        bool Complete { get; }
        bool MoveNext(double delta);

        IEnumerator WaitForComplete();
    }

    public abstract class Coroutine : ICoroutine
    {
        public bool Started { get; private set; }
        public bool Complete { get; private set; }

        public Coroutine()
        {
            this.Started = false;
            this.Complete = false;
        }

        public bool MoveNext(double delta)
        {
            var result = this.OnMoveNext(delta);
            this.Started = true;

            if (result)
            {
                return true;
            }
            else
            {
                this.Complete = true;
                return false;
            }

        }

        public IEnumerator WaitForComplete()
        {
            while (this.Complete == false)
            {
                yield return null;
            }

        }

        protected abstract bool OnMoveNext(double delta);
    }

}
