using UnityEngine;
using System.Collections.Generic;

public class ActorController : EntityController
{

    public ActorModel actorModel { get; private set; }

    protected Dictionary<EquipmentType, EquipSlotController> _equipSlotMap;

    protected override void Start()
    {
        base.Start();
        InitEquipments();
    }

    protected override void InitModel()
    {
        base.InitModel();
        actorModel = EntityModelBuilder.Instance.Create<ActorModel>(dataInfo);
        actorModel.onWeaponSet += OnWeaponSet;
        actorModel.onInitSlot += InitSlot;
    }


    #region WEAPON
    public WeaponController CurrentWeapon { get; private set; }
    private void OnWeaponSet(WeaponModel weapon)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Release();
        }
    }

    #endregion

    #region  EQUIPMENT 


    private void InitSlot(EquipSlotModel slotModel)
    {
        if (!_equipSlotMap.ContainsKey(slotModel.type))
        {
            Debug.Log(this.ToString() + "=>InitSlot: No slot of type:" + slotModel.type);
            return;
        }
        _equipSlotMap[slotModel.type].Init(slotModel);
    }

    private void InitEquipments()
    {
        var slots = GetComponentsInChildren<EquipSlotController>();
        _equipSlotMap = new Dictionary<EquipmentType, EquipSlotController>();
        foreach(EquipSlotController slot in slots){
            _equipSlotMap.Add(slot.equipType, slot);
        }
    }


   /* public void Equip(GameObject equipPrefab, Action callback = null)
    {
        var ec = equipPrefab.GetComponent<BaseEquipmentController>();
        _equipSlotMap[ec.equipType].EquipItem(equipPrefab);
        if (callback != null)
        {
            callback.Invoke();
            callback = null;
        }
    }*/

  /*  private void Unequip(EquipmentType equipType)
    {
        if (!_equipSlotMap.ContainsKey(equipType)) {
            Debug.LogError(string.Format("Can't unequip {0} from {1}. There is no suitable equip holder."));
            return;
        }
        _equipSlotMap[equipType].UnequipItem();
    }*/

    protected void UnequipAll()
    {
        foreach (var holder in _equipSlotMap.Values)
        {
            holder.slotModel.Unequip();
        }
    }

    #endregion


}
