using System;
using UnityEngine;

public enum ENTITY_TYPE
{
    PLAYER,
    ENEMY,
    BONUS
}

[Serializable]
public class BaseData  : ScriptableObject
{
    public int ID { get { return id; } }
    public int id;
    public string prefabName;
    public string prefabFullPath;
}


public class DataBase : ScriptableObject
{
}
