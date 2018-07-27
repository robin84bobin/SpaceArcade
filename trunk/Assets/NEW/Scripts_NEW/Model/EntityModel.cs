using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Text;


public class EntityModel: IParamOwner
{
    public bool enableDamage;

    public string type { get; protected set; }

    public Dictionary<string, Parameter> parameters { get; private set; }
    public event Action<Parameter> onParamAttached = delegate { };
    public event Action<Parameter> onParamDetached = delegate { };

    public event Action<DamageType, float> onDamage = delegate { };
    protected void OnDamage(DamageType type, float value) { onDamage.Invoke(type, value); }

    public event Action<DamageType> onDotAdd = delegate { };
    protected void OnDotAdd(DamageType type) { onDotAdd.Invoke(type); }

    public event Action<DamageType> onDotRemove = delegate { };
    protected void OnDotRemove(DamageType type) { onDotRemove.Invoke(type); }

    public event Action onDefeat = delegate { };
    protected virtual void OnDefeat() { onDefeat.Invoke(); }

    public Parameter GetParam(string name)
    {
        return parameters.ContainsKey(name) ? parameters[name] : null;
    }

    public void Init()
    {
        InitParams();
        InitDamageMap();
    }

    public virtual void Update()
    {
        foreach (var dot in _dotMap) dot.Value.Update();
    }

    #region Parameters  

    /// <summary>
    /// Fill parameter map using reflection
    /// </summary>
    private void InitParams()
    {
        parameters = new Dictionary<string, Parameter>();
        FieldInfo[] fields = this.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++)
            if (fields[i].FieldType.Equals(typeof(Parameter)))
            {
                Parameter param = fields[i].GetValue(this) as Parameter;
                parameters.Add(param.name, param);
            }

        InitParamsCallbacks();
        ResetParams();
    }

    protected void ResetParams()
    {
        foreach (var p in parameters)
        {
            p.Value.Reset();
        }
    }

    public void AttachParam(Parameter param)
    {
        if (!parameters.ContainsKey(param.name))
        {
            parameters.Add(param.name, param);
            onParamAttached.Invoke(param);
        }
        else
        {
            Debug.LogError("Can't attach parameter! Parameter already created : " + param.name);
        }
    }

    public void DetachParam(Parameter param)
    {
        if (parameters.ContainsKey(param.name))
        {
            parameters.Remove(param.name);
            onParamDetached.Invoke(param);
        }
        else
        {
            Debug.Log("Can't detach parameter! Parameter not created : " + param.name);
        }
    }

    private void InitParamsCallbacks()
    {
        //Set callback to invoke death
        //to Health parameter 
        if (parameters.ContainsKey(ParamName.HEALTH))
        {
            parameters[ParamName.HEALTH].onMinValue += OnDefeat;
        }
        else
        //or to Armor parameter if there is no Health (e.g. for Asteroid)
        if (parameters.ContainsKey(ParamName.ARMOR))
        {
            parameters[ParamName.ARMOR].onMinValue += OnDefeat;
        }
    }

    #endregion

    #region Damage

    Dictionary<DamageType, DamageProcessor> _damageMap;
    private void InitDamageMap()
    {
        _damageMap = new Dictionary<DamageType, DamageProcessor>();
        _dotMap = new Dictionary<DamageType, DotDamage>();

        //Hit damage affects armor first health next
        _damageMap.Add(DamageType.HIT, new DamageProcessor(this, new string[2] { ParamName.ARMOR, ParamName.HEALTH }, ParamChangeStrategies.Serial));

        //Poison affects only health
        _damageMap.Add(DamageType.POISON, new DamageProcessor(this, new string[1] { ParamName.HEALTH }, ParamChangeStrategies.Serial));

        //Fire affects armor and health simultaneously
        _damageMap.Add(DamageType.FIRE, new DamageProcessor(this, new string[2] { ParamName.ARMOR, ParamName.HEALTH }, ParamChangeStrategies.Parallel));
        enableDamage = true;
    }

    internal void ApplyDamage(DamageType damageType, float damageValue)
    {
        if (enableDamage && _damageMap.ContainsKey(damageType))
        {
            _damageMap[damageType].Apply(damageValue);
            OnDamage(damageType, damageValue);
        }
    }

    #endregion

    #region DotDamage

    private Dictionary<DamageType, DotDamage> _dotMap;

    public void AddDot(DamageType damageType, float damageValue, float applyTime, float applyPeriod)
    {
        if (!enableDamage || _dotMap.ContainsKey(damageType)) //do not attach similar dot twice
        {
            return;
        }

        DotDamage dotDamage = new DotDamage();
        dotDamage.Init(this, damageType, damageValue, applyTime, applyPeriod);
        _dotMap.Add(damageType, dotDamage);
        onDotAdd(damageType);
    }

    public void RemoveDot(DotDamage dotDamage)
    {
        if (_dotMap.ContainsKey(dotDamage.DamageType))
        {
            _dotMap.Remove(dotDamage.DamageType);
        }
        OnDotRemove(dotDamage.DamageType);
    }

    protected void RemoveAllDots()
    {
        foreach (var dot in _dotMap)
        {
            RemoveDot(dot.Value);
        }
    }



    #endregion

    public void Release()
    {
        onParamAttached = delegate { };
        onParamDetached = delegate { };
        onDefeat = delegate { };
    }
}

