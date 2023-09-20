using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActorModel : EntityModel
{
    protected ActorData _data;

    public event Action<EquipSlotModel> onInitSlot = delegate { };

    
    public ActorModel(EntityData data = null):base()
    {
        if (data != null) Init(data as ActorData);
    }

    public void Init(ActorData data)
    {
        base.Init();
        _data = data;
        InitEquipSlots();
    }

    #region WEAPON
    public event Action<WeaponModel> onWeaponSet = delegate { };
    public WeaponModel currentWeapon { get; private set; }
    private void SetCurrentWeapon(EquipModel weapon)
    {
        currentWeapon = weapon as WeaponModel;
        onWeaponSet.Invoke(currentWeapon);
    }

    public void Attack()
    {
        if (currentWeapon != null)
        {
            currentWeapon.TryAttack();
        }
    }

    private EquipSlotModel weaponSlot;

#endregion


#region EQUIPMENT
    protected Dictionary<EquipmentType, EquipSlotModel> _equipSlotMap;

    public void InitEquipSlots()
    {
        _equipSlotMap = new Dictionary<EquipmentType, EquipSlotModel>();
        EquipmentType type;
        for (int i = 0; i < _data.equipSlotTypes.Length; i++)
        {
            type = _data.equipSlotTypes[i];
            EquipSlotModel slotModel = new EquipSlotModel(type);
            _equipSlotMap.Add(type, slotModel);
            onInitSlot.Invoke(slotModel);
        }

        //Init weapon slot
        if (_equipSlotMap.ContainsKey(EquipmentType.WEAPON))
        {
            weaponSlot = _equipSlotMap[EquipmentType.WEAPON];
            weaponSlot.onEquipItem += SetCurrentWeapon;
            weaponSlot.onUnequipItem += (w) => { SetCurrentWeapon(null); };
        }
    }

    public void Equip(EquipData equipData)
    {
        if (!_equipSlotMap.ContainsKey(equipData.equipType))
        {
            Debug.Log("Cant equip:" + equipData.equipType.ToString() + ". No proper slot.");
            return;
        }

        _equipSlotMap[equipData.equipType].TryEquipItem(equipData);
    }

    public void UnequipAll()
    {
        foreach (var slot in _equipSlotMap)
        {
            slot.Value.Unequip();
        }
    }
#endregion


    public void Release()
    {
        foreach (var slot in _equipSlotMap)
        {
            slot.Value.Release();
        }
    }
}

