using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

public class Pool<T> where T : class
{
    List<T> values = new();

    public void Add(T item)
    {
        values.Add(item);
    }

    public T GetLast()
    {
        if (values.Count > 0)
            return values.Last();
        return null;
    }

    public T GetElementAt(int i)
    {
        if (i >= values.Count || i < 0)
            return null;
        T item = values[i];
        values.RemoveAt(i);
        return item;
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if(values[i] == item)
                return i;
        }
        return -1;
    }

    public bool Contains(T item)
    {
        if(IndexOf(item) < 0)
            return false;
        return true;
    }

}
