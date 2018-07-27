using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

[InitializeOnLoad]
public class DataController
{
    private static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataController();
                _instance.Init();
            }
            return _instance;
        }
    }

    private Dictionary<string, IBaseStorage> _dataStoragesMap;


    public void Init()
    {
        _dataStoragesMap = new Dictionary<string, IBaseStorage>();
        //test
        BaseStorage<EntityData> enemyStorage = new BaseStorage<EntityData>();
        enemyStorage.Add(1, new EntityData() { entity_class = ENTITY_TYPE.ENEMY });
        enemyStorage.Add(2, new EntityData() { entity_class = ENTITY_TYPE.ENEMY });
        _dataStoragesMap.Add(typeof(EntityData).Name, enemyStorage);

        BaseStorage<ActorData> actorStorage = new BaseStorage<ActorData>();
        actorStorage.Add(1, new ActorData() { entity_class = ENTITY_TYPE.ENEMY });
        actorStorage.Add(2, new ActorData() { entity_class = ENTITY_TYPE.ENEMY });
        actorStorage.Add(3, new ActorData() { entity_class = ENTITY_TYPE.ENEMY });
        _dataStoragesMap.Add(typeof(ActorData).Name, actorStorage);
        //
    }

    public string[] GetStoragesTypes()
    {
        int count = _dataStoragesMap.Keys.Count;
        string[] types = new string[count];
        _dataStoragesMap.Keys.CopyTo(types, 0);
        return types;
    }

    /*public string[] GetStoragesTypesNames()
    {
        Type[] types = GetStoragesTypes();
        string[] typeNames = new string[types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            typeNames[i] = types[i].Name;
        }
        return typeNames;
    }*/

    public IBaseStorage Storage(string typeName)
    {
        return _dataStoragesMap[typeName];
    }

    public T Get<T>(int id) where T :BaseData
    {
        return _dataStoragesMap[typeof(T).Name].Get(id) as T;
    }
}

