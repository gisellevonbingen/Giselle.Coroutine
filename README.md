# Giselle.Coroutine

Unity Style Coroutine For C#


```CSharp

public static void Main()
{
	var timer = new Timer(OnTimerTick);
	timer.Change(new TimeSpan(), TimeSpan.FromMilliseconds(1.0D / Stopwatch.Frequency * 100000000));

	Manager.Start(Test());

	Console.ReadLine();
}

private static IEnumerator Test()
{
	Console.WriteLine("A");

	var r = Manager.Start(Test2());
	yield return r;

	Console.WriteLine("Z");
}

private static IEnumerator Test2()
{
	yield return new WaitDuration(1000);
	Console.WriteLine("B");
	yield return new WaitDuration(1000);
	Console.WriteLine("C");
	yield return new WaitDuration(1000);
	Console.WriteLine("D");
	yield return new WaitDuration(1000);
	Console.WriteLine("E");
}

```
