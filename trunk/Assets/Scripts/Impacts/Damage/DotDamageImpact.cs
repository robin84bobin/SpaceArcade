using System;

public class DotDamageImpact : DamageImpact
{
    protected DamageType _damageType;
    public float applyPeriod = 1;
    public float applyTime = 5;

    public override DamageType damageType
    {
        get
        {
            return _damageType;
        }
    }

    protected override void OnApply(EntityModel entity)
    {
        if (!entity.enableDamage || !CheckTarget(entity)) return;

        entity.AddDot(damageType, damageValue, applyTime, applyPeriod);
        OnAppliedComplete();
    }


    protected override void OnAppliedComplete()
    {
        if (destroyOnApplied)
            ObjectPool.instance.PoolObject(gameObject);
    }
}
