using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Coroutine
{
    public interface ICoroutineAction
    {
        bool MoveNext(double delta, Coroutine coroutine);
    }

    public interface ICoroutineAction<T> : ICoroutineAction
    {

    }

    public abstract class CoroutineAction : ICoroutineAction
    {
        public abstract bool MoveNext(double delta, Coroutine coroutine);

        public static implicit operator CoroutineAction(Routine routine)
        {
            return new CoroutineActionRoutine(routine);
        }

        public static implicit operator CoroutineAction(Coroutine routine)
        {
            return new CoroutineActionCoroutine(routine);
        }

        public static CoroutineAction Enumerator(IEnumerator routine)
        {
            return new CoroutineActionIEnumerator(routine);
        }

    }

    public abstract class CoroutineAction<T> : CoroutineAction, ICoroutineAction<T>
    {
        public static implicit operator CoroutineAction<T>(T value)
        {
            return new CoroutineActionResult<T>(value);
        }

        public static implicit operator CoroutineAction<T>(Routine routine)
        {
            return new CoroutineActionRoutine<T>(routine);
        }

        public static implicit operator CoroutineAction<T>(Coroutine routine)
        {
            return new CoroutineActionCoroutine<T>(routine);
        }

        public static CoroutineAction<T> Enumerator(IEnumerator routine)
        {
            return new CoroutineActionIEnumerator<T>(routine);
        }

        public static CoroutineAction<T> Enumerator(IEnumerator<CoroutineAction<T>> routine)
        {
            return new CoroutineActionIEnumerator<T>(routine);
        }

    }

    public class CoroutineActionCoroutine : CoroutineAction
    {
        public ICoroutine Coroutine { get; private set; }

        public CoroutineActionCoroutine(ICoroutine routine)
        {
            this.Coroutine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return this.Coroutine.Complete == false;
        }

    }

    public class CoroutineActionCoroutine<T> : CoroutineAction<T>
    {
        public ICoroutine Coroutine { get; private set; }

        public CoroutineActionCoroutine(ICoroutine routine)
        {
            this.Coroutine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return this.Coroutine.Complete == false;
        }

    }

    public class CoroutineActionRoutine : CoroutineAction
    {
        public Routine Routine { get; private set; }

        public CoroutineActionRoutine(Routine routine)
        {
            this.Routine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return this.Routine.MoveNext(delta);
        }

    }

    public class CoroutineActionRoutine<T> : CoroutineAction<T>
    {
        public Routine Routine { get; private set; }

        public CoroutineActionRoutine(Routine routine)
        {
            this.Routine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return this.Routine.MoveNext(delta);
        }

    }

    public class CoroutineActionIEnumerator : CoroutineAction
    {
        public IEnumerator Routine { get; private set; }

        public CoroutineActionIEnumerator(IEnumerator routine)
        {
            this.Routine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return coroutine.MoveSubroutine(delta, this.Routine);
        }

    }

    public class CoroutineActionIEnumerator<T> : CoroutineAction<T>
    {
        public IEnumerator Routine { get; private set; }

        public CoroutineActionIEnumerator(IEnumerator routine)
        {
            this.Routine = routine;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return coroutine.MoveSubroutine(delta, this.Routine);
        }

    }

    public class CoroutineActionResult<T> : CoroutineAction<T>
    {
        public T Value { get; private set; }

        public CoroutineActionResult(T value)
        {
            this.Value = value;
        }

        public override bool MoveNext(double delta, Coroutine coroutine)
        {
            return false;
        }

    }

}
