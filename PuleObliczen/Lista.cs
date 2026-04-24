using System;
using System.Collections.Concurrent;
using System.Threading;

public class ComputationPool<T> where T : class, IComputation
{
    private readonly ConcurrentBag<T> _pool = new ConcurrentBag<T>();
    private readonly T _prototype;
    private int _createdObjectsCount = 0; 

    public int TotalCreated => _createdObjectsCount;

    public ComputationPool(T prototype) => _prototype = prototype;

    public T Get()
    {
        if (_pool.TryTake(out T item)) return item;
        Interlocked.Increment(ref _createdObjectsCount);
        return (T)_prototype.Clone();
    }

    public void Return(T item)
    {
        item.Reset(); 
        _pool.Add(item);
    }
}