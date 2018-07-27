using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[Serializable]
public class EntityDataInfo
{
    public int id;
    public string storageType;
}

[Serializable]
public class EntityData : BaseData
{
    public ENTITY_TYPE entity_class;
}

[Serializable]
public class ActorData : EntityData
{
    public EquipmentType[] equipSlotTypes;
    public string prefab;
}
