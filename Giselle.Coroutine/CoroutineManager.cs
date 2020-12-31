using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public class CoroutineManager
    {
        private readonly List<ICoroutine> Coroutines;
        public long Rivision { get; private set; }

        public CoroutineManager()
        {
            this.Coroutines = new List<ICoroutine>();
        }

        public ICoroutine Start(IEnumerator routine)
        {
            lock (this.Coroutines)
            {
                var coroutine = new Coroutine(routine);
                this.Coroutines.Add(coroutine);
                return coroutine;
            }

        }

        public ICoroutine<T> Start<T>(IEnumerator<CoroutineAction<T>> routine)
        {
            lock (this.Coroutines)
            {
                var coroutine = new Coroutine<T>(routine);
                this.Coroutines.Add(coroutine);
                return coroutine;
            }

        }

        public bool Stop(ICoroutine coroutine)
        {
            lock (this.Coroutines)
            {
                return this.Coroutines.Remove(coroutine);
            }

        }

        public void StopAll()
        {
            lock (this.Coroutines)
            {
                this.Coroutines.Clear();
            }

        }

        public void Update(double delta)
        {
            lock (this.Coroutines)
            {
                this.Rivision++;

                foreach (var coroutine in this.Coroutines.ToArray())
                {
                    if (coroutine.MoveNext(delta) == false)
                    {
                        this.Coroutines.Remove(coroutine);
                    }

                }

            }

        }

    }

}
