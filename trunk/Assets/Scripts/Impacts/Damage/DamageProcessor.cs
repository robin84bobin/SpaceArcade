
public class DamageProcessor
{
    private IParamOwner _target;
    private readonly string[] _params;
    private readonly IParamChangeStrategy _strategy;

    public DamageProcessor(IParamOwner target, string[] afffectedParams, IParamChangeStrategy strategy)
    {
        _target = target;
        _params = afffectedParams;
        _strategy = strategy;
    }

    public void Apply(float value)
    {
        _strategy.Apply(- value, _params, _target.parameters);
    }
}