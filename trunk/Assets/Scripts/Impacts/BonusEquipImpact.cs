using System;
using UnityEngine;

public class BonusEquipImpact : CollisionImpact
{
    public EquipData equipData;

    protected override void OnApply(EntityModel entity)
    {
        //if (!CheckTarget(entity)) return;
        Equip();
        OnAppliedComplete();
    }
    
    private void Equip()
    {
        _targetActor.Equip(equipData);
    }

    private ActorModel _targetActor;

    protected override bool CheckTarget(EntityModel entity)
    {
        _targetActor = entity as ActorModel;
        if (_targetActor == null)
        {
            Debug.Log(string.Format("Can't apply {0} to {1}", this.ToString(), entity.ToString()));
            return false;
        }

        return true && base.CheckTarget(entity);
    }
}

