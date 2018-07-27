/*using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

internal class EnemyBuilder : ConcreteEntityBuilder
{
    public override GameObject Create(int id)
    {
        EnemyData data = Main.Instance.Data.Storage("Enemy").Get(id) as EnemyData;
        GameObject go = ObjectPool.instance.GetObject(data.prefab);

        //equip enemy
        ActorController actorController = go.GetComponent<ActorController>();
        if (data.equip != null && data.equip.Length > 0)
        {
            foreach (EquipData equip in data.equip)
            {
                GameObject equipPrefab = EntityBuilder.Instance.Create(equip.type, equip.id);
                actorController.Equip(equipPrefab);
            }
        }

        //create drop
        DropController dropController = go.GetComponent<DropController>();
        if (data.dropPrefabs != null && data.dropPrefabs != null && data.dropPrefabs.Length > 0)
        {
            dropController.dropPrefabNames = new List<string>();
            for (int i = 0; i < data.dropPrefabs.Length; i++)
            {
                dropController.dropPrefabNames.Add(data.dropPrefabs[i]);
            }
        }

        return go;
    }
}
*/