using System.Collections.Generic;

public interface IBaseStorage
{
    BaseData Get(int id);
    void Add(int id, BaseData data);
    int[] GetIds();
}

public class BaseStorage<T> : IBaseStorage where T:BaseData , new()
{
    protected Dictionary<int, T> _dataMap;
    public BaseData Get(int id) 
    {
        return _dataMap[id];
    }

    public void Add(int id, BaseData data)
    {
        _dataMap.Add(id, data as T);
    }

    public BaseStorage()
    {
        _dataMap = new Dictionary<int, T>();
    }

    public int[] GetIds()
    {
        int[] ids = new int[_dataMap.Keys.Count];
        _dataMap.Keys.CopyTo(ids, 0);
        return ids;
    }
}

