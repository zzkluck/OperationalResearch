using System;
using System.Linq;
using System.Collections.Generic;

// A Stream is an infinite sequence of items
// It is defined recursively as a head item followed by a Stream
// The Stream tail is wrapped in a Lazy object so that it can be lazily evaluated
public class Stream<T>
{
	public readonly T Head;
	public readonly Lazy<Stream<T>> Tail;

	public Stream(T head, Lazy<Stream<T>> tail)
	{
		Head = head;
		Tail = tail;
	}
}

static class Stream
{
	// First let's define an utility function to construct a Stream given a head and a function returning a tail
	public static Stream<T> Cons<T>(T h, Func<Stream<T>> t)
	{
		return new Stream<T>(h, new Lazy<Stream<T>>(t));
	}

	// .------------------------------.
	// | Static constructor functions |
	// '------------------------------'

	// Construct a stream by repeating a value.
	public static Stream<T> Repeat<T>(T x)
	{
		return Cons(x, () => Repeat(x));
	}

	// Construct a stream by repeatedly applying a function.
	public static Stream<T> Iterate<T>(Func<T, T> f, T x)
	{
		return Cons(x, () => Iterate(f, f(x)));
	}

	// Construct a stream by repeating an enumeration forever.
	public static Stream<T> Cycle<T>(IEnumerable<T> a)
	{
		var b = a.RepeatIndefinitely();
		return Cons(b.First(), () => Cycle(b.Skip(1)));
	}

	private static IEnumerable<T> RepeatIndefinitely<T>(this IEnumerable<T> source)
	{
		while (true)
		{
			foreach (var item in source) yield return item;
		}
	}

	// Construct a stream by counting numbers starting from a given one.
	public static Stream<int> From(int x)
	{
		return Cons(x, () => From(x + 1));
	}

	// Same as From but count with a given step width.
	public static Stream<int> FromThen(int x, int d)
	{
		return Cons(x, () => FromThen(x + d, d));
	}

	// .------------------------------------------.
	// | Stream reduction and modification (pure) |
	// '------------------------------------------'

	// Being applied to a stream (x1, x2, x3, ...), Foldr shall return f(x1, f(x2, f(x3, ...)))
	// It is called Fold "right" because the function f is first applied to the "last" item of the stream
	// Streams having no "last" item, it will only work for functions f where, at some point,
	// f(xN, f(xN+1, ...)) can be computed without computing f(xN+1, ...)
	public static U Foldr<T, U>(this Stream<T> s, Func<T, Func<U>, U> f)
	{
		return f(s.Head, () => s.Tail.Value.Foldr(f));
	}

	// Filter stream with a predicate function.
	public static Stream<T> Filter<T>(this Stream<T> s, Predicate<T> p)
	{
		if (p(s.Head)) return Cons(s.Head, () => s.Tail.Value.Filter(p));
		return s.Tail.Value.Filter(p);
	}

	// Returns a given amount of elements from the stream.
	public static IEnumerable<T> Take<T>(this Stream<T> s, int n)
	{
		if (n > 0)
		{
			yield return s.Head;
			foreach (var i in s.Tail.Value.Take(n - 1)) yield return i;
		}
	}

	// Drop a given amount of elements from the stream.
	public static Stream<T> Drop<T>(this Stream<T> s, int n)
	{
		return (n > 0) ? s.Tail.Value.Drop(n - 1) : s;
	}

	// Combine 2 streams with a function.
	public static Stream<T> ZipWith<T>(this Stream<T> s, Func<T, T, T> f, Stream<T> other)
	{
		return Cons(f(s.Head, other.Head), () => ZipWith(s.Tail.Value, f, other.Tail.Value));
	}

	// Map every value of the stream with a function, returning a new stream.
	public static Stream<T> FMap<T>(this Stream<T> s, Func<T, T> f)
	{
		return Cons(f(s.Head), () => FMap(s.Tail.Value, f));
	}

	// Return the stream of all fibonacci numbers.
	public static Stream<int> Fib()
	{
		return Cons(0, () => Cons(1, () => Fib(0, 1)));
	}

	private static Stream<int> Fib(int n, int n1)
	{
		return Cons(n + n1, () => Fib(n1, n + n1));
	}

	// Return the stream of all prime numbers.
	public static Stream<int> Primes()
	{
		return From(2).Filter(i => IsPrime(i));
	}

	private static bool IsPrime(int z)
	{
		if (z > 3)
		{
			if (z % 2 == 0) return false;

			int sqrt = (int)Math.Floor(Math.Sqrt(z));
			for (int i = 3; i <= sqrt; i += 2)
			{
				if (z % i == 0) return false;
			}
		}

		return true;
	}
}
