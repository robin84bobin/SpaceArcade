using System;

public class HelmetController : BaseEquipmentController
{
    public float armorValue = 50;
    private float _currentArmor;

    private Parameter armor;
    private ActorModel _owner;

    public override EquipmentType equipType
    {
        get
        {
            return EquipmentType.ARMOR;
        }
    }

    void Awake()
    {
        _currentArmor = armorValue;
        armor = new Parameter(ParamName.ARMOR, armorValue, armorValue, 0f);
        armor.onMinValue += equipModel.Unequip;
    }

    private void OnArmorChange(float val1, float val2)
    {
    }

    public override void OnEquip(ActorModel owner)
    {
        _owner = owner;
        _owner.AttachParam(armor);
    }


    public override void OnUnequip()
    {
        _owner.DetachParam(armor);
        armor.Release();
    }

    public override void Release()
    {
        base.Release();
        _owner = null;
    }
}
