
public class PoisonImpact : DotDamageImpact
{
    void Awake()
    {
        _damageType = DamageType.POISON;
    }

    void Reset()
    {
        applyPeriod = 1;
        applyTime = 5;
        damageValue = 10;
    }
}

