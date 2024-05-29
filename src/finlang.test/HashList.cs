// from StateSmith project
namespace finlang.test;

public class HashList<K, V> where K : notnull
{
    private Dictionary<K, List<V>> dictionary = new();

    public IEnumerable<IReadOnlyList<V>> GetValues()
    {
        return dictionary.Values;
    }

    public List<V> GetValues(K key)
    {
        return dictionary[key];
    }

    public List<K> GetKeys()
    {
        return dictionary.Keys.ToList();
    }

    public List<V> GetValuesOrEmpty(K key)
    {
        if (Contains(key))
        {
            return GetValues(key);
        }
        return new List<V>();
    }

    public bool Contains(K key)
    {
        return dictionary.ContainsKey(key);
    }

    public void Add(K key, V value)
    {
        if (dictionary.ContainsKey(key))
        {
            List<V> list = dictionary[key];
            list.Add(value);
        }
        else
        {
            dictionary[key] = new List<V>()
            {
                value
            };
        }
    }

    public void AddIfValueMissing(K key, V value)
    {
        if (dictionary.ContainsKey(key))
        {
            List<V> list = dictionary[key];
            if (list.Contains(value) == false)
            {
                list.Add(value);
            }
        }
        else
        {
            dictionary[key] = new List<V>()
            {
                value
            };
        }
    }
}
