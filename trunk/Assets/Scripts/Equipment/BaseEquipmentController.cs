using System;
using UnityEngine;

public abstract class BaseEquipmentController : MonoBehaviour
{
    protected EquipModel equipModel;
    protected ActorModel _owner;

    private bool unequipAnimation;
    private bool equipAnimation;

    public abstract EquipmentType equipType { get; }
    public abstract void OnEquip(ActorModel owner);

    public event Action onUnequipComplete = delegate { };
    protected void OnUnequipComplete() { onUnequipComplete.Invoke(); }

    public BaseEquipmentController(EquipModel model = null)
    {
        if (model != null) Init(model);
    }

    public virtual void Init(EquipModel model)
    {
        equipModel = model;
        equipModel.onUnequip += OnUnequip;
    }

    public virtual void OnUnequip()
    {
        equipModel.Release();
        if (unequipAnimation)  {
            /* TODO invoke OnUnequipComplete() on animation end; */
        }
        else
            OnUnequipComplete();
    }

    public virtual void Release()
    {
        onUnequipComplete = delegate { };
        equipModel = null;
    }
}