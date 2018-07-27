
public class FireImpact : DotDamageImpact
{
    void Awake()
    {
        _damageType = DamageType.FIRE;
    }

    void Reset()
    {
        applyPeriod = 0.2f;
        applyTime = 5;
        damageValue = 2;
    }
}
