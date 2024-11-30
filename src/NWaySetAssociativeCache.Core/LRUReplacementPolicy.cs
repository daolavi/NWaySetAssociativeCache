namespace NWaySetAssociativeCache.Core;

public class LRUReplacementPolicy<TKey> : IReplacementPolicy<TKey>
{
    private LinkedList<TKey> _list;

    public LRUReplacementPolicy()
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
            throw new InvalidOperationException("No items found");
        }
        return _list.Last.Value;
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