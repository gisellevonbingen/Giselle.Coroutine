# Giselle.Coroutine

Unity Style Coroutine For C#

## Examples

### Coroutine Manager, Execute Coroutine
```CSharp

public class Program
{
	public static CoroutineManager Manager { get; private set; }
	public static Stopwatch Stopwatch { get; private set; }
	public static double LastMillis { get; private set; }

	public static void Main()
	{
		Manager = new CoroutineManager();
		Stopwatch = new Stopwatch();
		LastMillis = 0.0D;

		var timer = new Timer(OnTimerTick);
		timer.Change(new TimeSpan(), TimeSpan.FromMilliseconds(1.0D / Stopwatch.Frequency * 100000000));

		Manager.Start(TestSimple.Test());

		Console.ReadLine();
	}

	private static void OnTimerTick(object sender)
	{
		var delta = 0.0D;

		if (Stopwatch.IsRunning == false)
		{
			Stopwatch.Restart();
		}
		else
		{
			var millis = Stopwatch.Elapsed.TotalMilliseconds;
			delta = millis - LastMillis;
			LastMillis = millis;
		}

		Manager.Update(delta);
	}

}

```

### Unity Style Usage

```CSharp
Manager.Start(TestSimple.Test());
```
```CSharp
public static class TestSimple
{
	public static IEnumerator Test()
	{
		for (var i = 0; i < 4; i++)
		{
			Console.WriteLine("=== TEST " + i + " ===");
			yield return Test2(i + 1);
			yield return new WaitDuration(500);
		}

	}

	public static IEnumerator Test2(int count)
	{
		for (var i = 0; i < count; i++)
		{
			yield return new WaitDuration(500);
			Console.WriteLine("TEST2 : " + i);
		}

	}

}
```
Will print at 500ms intervals like below
```
=== TEST 0 ===
TEST2 : 0
=== TEST 1 ===
TEST2 : 0
TEST2 : 1
=== TEST 2 ===
TEST2 : 0
TEST2 : 1
TEST2 : 2
=== TEST 3 ===
TEST2 : 0
TEST2 : 1
TEST2 : 2
TEST2 : 3
```

### Fetech Result Values From Coroutine

```CSharp
Manager.Start(TestFetchResult.Test());
```
```CSharp
public static class TestFetchResult
{
	public static IEnumerator Test()
	{
		Console.WriteLine("A");

		var r = Program.Manager.Start(Test2());
		yield return r;

		// Fetch collected values
		foreach (var str in r.Results)
		{
			Console.WriteLine(str);
			yield return new WaitDuration(500);
		}

		Console.WriteLine("Z");
	}

	public static IEnumerator<CoroutineAction<string>> Test2()
	{
		// Delay 1000ms
		yield return new WaitDuration(1000);
		// Coroutine collect 'B' as result values
		yield return "B";
		yield return new WaitDuration(1000);
		yield return "C";
		yield return new WaitDuration(1000);
		yield return "D";
		yield return new WaitDuration(1000);
		yield return "E";
		yield return new WaitDuration(1000);
		yield return Test3().ToAction<string>();
	}

	public static IEnumerator<CoroutineAction<string>> Test3()
	{
		yield return "F";
	}

}
```
Will print like below
```
A
B
C
D
E
F
Z
```

### Mix IEnumerator<CoroutineAction<T>>, IEnumerator
	
```CSharp
public static class TestFetchResult
{
	public static IEnumerator Test()
	{
		Console.WriteLine("A");

		var r = Program.Manager.Start(Test2());
		yield return r;

		foreach (var str in r.Results)
		{
			Console.WriteLine(str);
			yield return new WaitDuration(500);
		}

		Console.WriteLine("Z");
	}

	public static IEnumerator<CoroutineAction<string>> Test2()
	{
		// Coroutine collect 'B' as result values
		yield return "B";
		yield return "C";
		yield return "D";
		yield return "E";
		yield return Test3().ToAction<string>();
	}

	public static IEnumerator Test3()
	{
		// Delay 1000ms
		yield return new WaitDuration(1000);
		// 'F' not collect
		yield return "F";
	}

}
```
Will print like below
```
A
B
C
D
E
Z
```
