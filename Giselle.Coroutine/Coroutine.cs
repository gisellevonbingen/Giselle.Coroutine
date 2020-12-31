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

        IEnumerator<CoroutineAction> WaitForComplete();
    }

    public interface ICoroutine<T> : ICoroutine
    {
        List<T> Results { get; }
    }

    public class Coroutine : ICoroutine
    {
        public bool Started { get; private set; }
        public bool Complete { get; private set; }

        public IEnumerator Routine { get; private set; }

        public Coroutine(IEnumerator routine)
        {
            this.Started = false;
            this.Complete = false;

            this.Routine = routine;
        }

        public bool MoveNext(double delta)
        {
            var result = this.MoveNext(delta, this.Routine);
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

        public IEnumerator<CoroutineAction> WaitForComplete()
        {
            while (this.Complete == false)
            {
                yield return null;
            }

        }

        private bool MoveNext(double delta, IEnumerator routine)
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

        public bool MoveSubroutine(double delta, object current)
        {
            if (current is CoroutineAction action)
            {
                this.OnAction(action);
                return action.MoveNext(delta, this);
            }
            else if (current is IEnumerator enumerator)
            {
                return this.MoveNext(delta, enumerator);
            }
            else if (current is Routine routine)
            {
                return routine.MoveNext(delta);
            }
            else if (current is ICoroutine coroutine)
            {
                return coroutine.Complete == false;
            }

            return false;
        }

        protected virtual void OnAction(CoroutineAction action)
        {

        }

    }

    public class Coroutine<T> : Coroutine, ICoroutine<T>
    {
        public List<T> Results { get; private set; }

        public Coroutine(IEnumerator<CoroutineAction<T>> routine)
            : base(routine)
        {
            this.Results = new List<T>();
        }

        protected override void OnAction(CoroutineAction action)
        {
            base.OnAction(action);

            if (action is CoroutineActionResult<T> result)
            {
                this.Results.AddRange(result.Values);
            }

        }

    }

}
