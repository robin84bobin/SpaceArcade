using System;
using System.Xml.Serialization;

[Serializable]
public class EnemyData : BaseData
{
    public int id;
    public string prefab;

    [XmlArray("equips")]
    [XmlArrayItem("equip")]
    public EquipData[] equip;

    [XmlArray("drop")]
    [XmlArrayItem("drop_prefab")]
    public string[] dropPrefabs;

    public int ID
    {
        get
        {
            return id;
        }
    }
}
