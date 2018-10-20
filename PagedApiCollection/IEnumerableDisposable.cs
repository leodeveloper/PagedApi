// DO NOT MODIFY THIS FILE
using System;
using System.Collections.Generic;

namespace PagedApiCollection
{
	public interface IEnumerableDisposable<out T> : IEnumerable<T>, IDisposable
	{
	}
}