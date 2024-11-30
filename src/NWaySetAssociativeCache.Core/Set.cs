namespace NWaySetAssociativeCache.Core;

public class Set<TKey, TValue> 
    where TKey : notnull
{
    private Dictionary<TKey, TValue> _dictionary;
    private readonly IReplacementPolicy<TKey> _replacementPolicy;
    private int _capacity;
    private object _lockObject = new();

    public Set(int capacity, IReplacementPolicy<TKey> replacementPolicy)
    {
        _capacity = capacity;
        _dictionary = new Dictionary<TKey, TValue>(capacity);
        _replacementPolicy = replacementPolicy;
    }
    
    public void Put(TKey key, TValue value)
    {
        lock (_lockObject)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
                _replacementPolicy.RecordAccess(key);
            }
            else
            {
                if (_dictionary.Count >= _capacity)
                {
                    var victim = _replacementPolicy.SelectVictim();
                    _dictionary.Remove(victim);
                    _replacementPolicy.Remove(victim);
                }

                _dictionary[key] = value;
                _replacementPolicy.Add(key);
            }
        }
    }

    public TValue Get(TKey key)
    {
        lock (_lockObject)
        {
            if (_dictionary.TryGetValue(key, out var value))
            {
                _replacementPolicy.RecordAccess(key);
                return value;
            }

            throw new KeyNotFoundException();
        }
    }
}