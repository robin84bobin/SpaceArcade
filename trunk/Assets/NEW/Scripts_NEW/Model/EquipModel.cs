using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EquipModel
{
    public event Action onUnequip = delegate { };
    internal void Unequip() { onUnequip.Invoke(); }

    public EquipmentType equipType { get; private set; }

    public EquipData data { get; private set; }


    public EquipModel(EquipData data = null)
    {
        if (data != null)  Init(data);
    }

    public virtual void Init(EquipData data)
    {
        this.data = data;
        equipType = this.data.equipType;
    }

    internal void Release()
    {
        onUnequip = delegate { };
        data = null;
    }
}

public class EquipFactory
{
    public static EquipModel Create(EquipData data)
    {
        switch(data.equipType)
        {
            case EquipmentType.WEAPON: return new WeaponModel(data);
            default: return new EquipModel(data);
        }
    }
}
