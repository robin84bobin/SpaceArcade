using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public  class EquipSlotModel
{
    public event Action<EquipModel> onEquipItem = delegate { };
    public event Action<EquipModel> onUnequipItem = delegate { };

    public EquipmentType type { get; private set; }
    public EquipModel equipModel { get; private set; }

    public EquipSlotModel(EquipmentType type)
    {
        this.type = type;
    }

    public void TryEquipItem(EquipData equipData)
    {
        if (!equipData.equipType.Equals(type))
        {
            Debug.Log("Cant equip:" + equipData.equipType.ToString() + " into slot:" + type.ToString());
            return;
        }

        if (equipModel == null)
        {
            Equip(equipData);
            return;
        }

        //cache itemData to equip after unequip complete
        _itemToEquip = equipData;
        Unequip();
    }

    public void Unequip()
    {
        equipModel.Unequip();
        equipModel = null;
    }

    EquipData _itemToEquip;
    public void UnequipComplete()
    {
        if (_itemToEquip != null)
        {
            Equip(_itemToEquip);
        }
    }

    public void Equip(EquipData equipData)
    {
        equipModel = EquipFactory.Create(_itemToEquip);
        onEquipItem.Invoke(equipModel);
    }

    internal void Release()
    {
        onEquipItem = delegate { };
        onUnequipItem = delegate { };
        equipModel = null;
    }
}
