using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public  class DropData
{
    [XmlArray("drop")]
    [XmlArrayItem("prefab")]
    public List<string> prefabs;
}
