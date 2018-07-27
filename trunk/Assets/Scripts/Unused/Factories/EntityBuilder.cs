/*using System.Collections.Generic;
using UnityEngine;

public class EntityBuilder
{
    private static EntityBuilder _instance;

    public static EntityBuilder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EntityBuilder();
            }
            return _instance;
        }
    }

    private Dictionary<string, ConcreteEntityBuilder> _factories;
    private EntityBuilder()
    {
        _factories = new Dictionary<string, ConcreteEntityBuilder>();
        _factories.Add("Enemy", new EnemyBuilder());
    }

    public GameObject Create(string type, int id)
    {
        return _factories[type].Create(id);
    }
}

internal abstract class ConcreteEntityBuilder
{
    public abstract GameObject Create(int id);
}*/