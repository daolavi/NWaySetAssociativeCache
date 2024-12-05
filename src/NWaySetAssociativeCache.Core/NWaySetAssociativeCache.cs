namespace NWaySetAssociativeCache.Core;

public class NWaySetAssociativeCache<TKey, TValue> 
    where TKey : notnull
{
    private int _numberOfSets;
    private int _setCapacity;
    private List<Set<TKey, TValue>> _sets;

    public NWaySetAssociativeCache(int numberOfSets, int setCapacity, Func<IReplacementPolicy<TKey>> replacementPolicyFactory)
    {
        _numberOfSets = numberOfSets;
        _setCapacity = setCapacity;
        _sets = new List<Set<TKey, TValue>>(numberOfSets);
        for (var i = 0; i < numberOfSets; i++)
        {
            _sets.Add(new Set<TKey, TValue>(_setCapacity, replacementPolicyFactory()));
        }
    }

    public void Put(TKey key, TValue value)
    {
        var set = _sets[GetSetIndex(key)];
        set.Put(key, value);
    }

    public TValue Get(TKey key)
    {
        var set = _sets[GetSetIndex(key)];
        return set.Get(key);
    }

    private int GetSetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % _numberOfSets; 
    }
}