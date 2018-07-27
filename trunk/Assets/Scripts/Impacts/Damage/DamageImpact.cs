public enum DamageType
{
    HIT,
    POISON,
    FIRE
}


public abstract class DamageImpact : CollisionImpact {

    public float damageValue;
    public abstract DamageType damageType { get; }

    protected override void OnApply(EntityModel entity)
    {
        if (!entity.enableDamage)
        {
            return;
        }
        entity.ApplyDamage(damageType, damageValue);
        OnAppliedComplete();
    }

}
