namespace NWaySetAssociativeCache.Core;

public class MRUReplacementPolicy<TKey> : IReplacementPolicy<TKey> where TKey : notnull
{
    private LinkedList<TKey> _list;

    public MRUReplacementPolicy()
    {
        _list = [];
    }
    
    public void RecordAccess(TKey key)
    {
        if (_list.Contains(key))
        {
            _list.Remove(key);
        }
        _list.AddFirst(key);
    }

    public TKey SelectVictim()
    {
        if (_list.Count == 0)
        {
            throw new InvalidOperationException("No item found");
        }
        return _list.First.Value;
    }

    public void Add(TKey key)
    {
        _list.AddFirst(key);
    }

    public void Remove(TKey key)
    {
        _list.Remove(key);
    }
}