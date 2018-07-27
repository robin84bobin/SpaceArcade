using UnityEngine;

public class DotDamage
{

    private EntityModel _target;
    public DamageType DamageType { get; private set; }
    private float _damageValue;
    private Timer _timer;

    public void Start () {
        _timer.Start();
	}
	
	public void Update () {
        _timer.Update();
	}


    public void Init(EntityModel target, DamageType damageType, float damageValue, float time, float period, float delay = 0f)
    {
        _target = target;
        DamageType = damageType;
        _damageValue = damageValue;

        _timer = new Timer(time, period, delay);
        _timer.OnStart(OnTimerStart).OnPeriod(OnTimerPeriod).OnComplete(OnTimerComplete);
    }

    private void OnTimerStart()
    {
        _target.ApplyDamage(DamageType, _damageValue);
    }

    private void OnTimerPeriod()
    {
        _target.ApplyDamage(DamageType, _damageValue);
    }

    void OnTimerComplete()
    {
        _timer.Release();
        _target.RemoveDot(this);
        Release();
    }

    void Release()
    {
        _target = null;
    }
}
