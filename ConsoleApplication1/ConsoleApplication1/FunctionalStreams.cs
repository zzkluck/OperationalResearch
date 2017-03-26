using System;
using System.Collections.Generic;

/*
    A Stream is an infinite sequence of items. It is defined recursively
    as a head item followed by the tail, which is another stream.
    Consequently, the tail has to be wrapped with Lazy to prevent
    evaluation.
*/
namespace aaa
{
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
		/*
			Your first task is to define a utility function which constructs a
			Stream given a head and a function returning a tail.
		*/
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
			Func<Stream<T>> repeat = null;
			repeat = delegate
			{
				return Cons(x, repeat);
			};
			return Cons(x, repeat);
		}

		// Construct a stream by repeatedly applying a function.
		public static Stream<T> Iterate<T>(Func<T, T> f, T x)
		{
			Func<Stream<T>> iterate = null;
			iterate = delegate
			{
				return Cons(f(x), iterate);
			};
			return Cons(x, iterate);
		}

		// Construct a stream by repeating an enumeration forever.
		public static Stream<T> Cycle<T>(IEnumerable<T> a)
		{
			Func<Stream<T>> cycle = null;
			IEnumerator<T> myEnumerator = a.GetEnumerator();
			cycle = delegate
			{
				if (!myEnumerator.MoveNext())
				{
					myEnumerator.Reset();
					myEnumerator.MoveNext();
				}
				return Cons(myEnumerator.Current, cycle);
			};
			return cycle();
		}

		// Construct a stream by counting numbers starting from a given one.
		public static Stream<int> From(int x)
		{
			return Iterate((n) => n + 1, x);
		}

		// Same as From but count with a given step width.
		public static Stream<int> FromThen(int x, int d)
		{
			return Iterate((n) => n + d, x);
		}

		// .------------------------------------------.
		// | Stream reduction and modification (pure) |
		// '------------------------------------------'

		/*
			Being applied to a stream (x1, x2, x3, ...), Foldr shall return
			f(x1, f(x2, f(x3, ...))). Foldr is a right-associative fold.
			Thus applications of f are nested to the right.
		*/
		public static U Foldr<T, U>(this Stream<T> s, Func<T, Func<U>, U> f)
		{
			//Foldr<T, Stream<T>>(s, (x, r) => Stream.Cons(x, () => r()));

			//return f(s.Head, s.Tail.Value);

			throw new NotImplementedException();
		}

		// Filter stream with a predicate function.
		public static Stream<T> Filter<T>(this Stream<T> s, Predicate<T> p)
		{
			throw new NotImplementedException();
		}

		// Returns a given amount of elements from the stream.
		public static IEnumerable<T> Take<T>(this Stream<T> s, int n)
		{
			throw new NotImplementedException();
		}

		// Drop a given amount of elements from the stream.
		public static Stream<T> Drop<T>(this Stream<T> s, int n)
		{
			throw new NotImplementedException();
		}

		// Combine 2 streams with a function.
		public static Stream<R> ZipWith<T, U, R>(this Stream<T> s, Func<T, U, R> f, Stream<U> other)
		{
			throw new NotImplementedException();
		}

		// Map every value of the stream with a function, returning a new stream.
		public static Stream<U> FMap<T, U>(this Stream<T> s, Func<T, U> f)
		{
			throw new NotImplementedException();
		}

		// Return the stream of all fibonacci numbers.
		public static Stream<int> Fib()
		{
			throw new NotImplementedException();
		}

		// Return the stream of all prime numbers.
		public static Stream<int> Primes()
		{
			throw new NotImplementedException();
		}
	}
}