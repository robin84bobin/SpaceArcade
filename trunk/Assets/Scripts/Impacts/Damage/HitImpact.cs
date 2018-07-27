
public class HitImpact : DamageImpact
{
    public override DamageType damageType
    {
        get
        {
           return DamageType.HIT;
        }
    }

    void Reset()
    {
        destroyOnApplied = true;
    }

    protected override void OnAppliedComplete()
    {
        if (destroyOnApplied)
            Destroy(this.gameObject);
    }
}
