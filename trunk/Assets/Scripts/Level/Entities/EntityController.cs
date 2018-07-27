using UnityEngine;
using System.Collections.Generic;
using System;


public class EntityController : MonoBehaviour
{
    [SerializeField]
    public EntityDataInfo dataInfo;

    [Header("Particle Effects")]
    public List<EffectInfo> effectList;

    public EntityModel model { get; private set; } 

    public event Action onAppear = delegate { };

    protected virtual void Start()
    {
        InitEffects();
        //
        onAppear.Invoke();
    }

    protected virtual void InitModel()
    {
        model = EntityModelBuilder.Instance.Create<EntityModel>(dataInfo);
        model.onDefeat += OnDeath;
        model.onDotAdd += AddDotEffect;
        model.onDotRemove += RemoveDotEffect;
    }

    #region Effects

    protected Dictionary<EffectType, ParticleSystem> _effectsMap;
    protected Dictionary<DamageType, EffectType> _dotEffectsMap;

    private void InitEffects()
    {
        _effectsMap = new Dictionary<EffectType, ParticleSystem>();
        for (int i = 0; i < effectList.Count; i++)
        {
            EffectInfo info = effectList[i];
            if (_effectsMap.ContainsKey(info.effectType))
            {
                Debug.LogError(string.Format("{1} : Dublicate effect of type: {0}", info.effectType.ToString(), this.GetType().Name));
            }
            _effectsMap.Add(info.effectType, info.particleSystem);
        }

        _dotEffectsMap = new Dictionary<DamageType, EffectType>();
        _dotEffectsMap.Add(DamageType.FIRE, EffectType.FIRE);
        _dotEffectsMap.Add(DamageType.POISON, EffectType.POISON);
    }

    void PlayDamageEffect(DamageType damageType, float damageValue)
    {
        if (_effectsMap.ContainsKey(EffectType.BLOOD))
            _effectsMap[EffectType.BLOOD].Emit((int)damageValue);
        else
            Debug.LogWarning(string.Format("{0}: effect not defined: {1}", this.GetType().Name, EffectType.BLOOD.ToString()));
    }

    protected void StopEffects()
    {
        foreach (ParticleSystem item in _effectsMap.Values)
        {
            item.Stop();
            item.Clear();
        }
    }

    #endregion

    #region DotEffects
    private void AddDotEffect(DamageType damageType)
    {
        if (_dotEffectsMap.ContainsKey(damageType))
        {
            EffectType effect = _dotEffectsMap[damageType];
            if (_effectsMap.ContainsKey(effect))
                _effectsMap[effect].Play();
            else
                Debug.LogWarning(string.Format("{0}: effect not defined: {1}", this.GetType().Name, effect.ToString()));
        }
    }

    private void RemoveDotEffect(DamageType damageType)
    {
        if (_dotEffectsMap.ContainsKey(damageType))
        {
            EffectType effect = _dotEffectsMap[damageType];
            if (_effectsMap.ContainsKey(effect))
            {
                _effectsMap[effect].Stop();
            }
        }
    }
    #endregion

    #region Collisions

    protected virtual void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other.gameObject);
    }

    protected void OnCollisionEnter(Collision other)
    {
        ProcessCollision(other.gameObject);
    }

    void ProcessCollision(GameObject otherGO)
    {
        var impacts = otherGO.GetComponents<CollisionImpact>();
        for (int i = 0; i < impacts.Length; ++i)
        {
            impacts[i].Apply(this.model);
        }
    }

    #endregion

    void OnGetFromPool()
    {
        onAppear.Invoke();
    }

    protected virtual void OnDeath()
    {
        SendMessage("OnDropReward", SendMessageOptions.DontRequireReceiver);
        Explode();
        ObjectPool.instance.PoolObject(gameObject);
    }

    protected virtual void Explode()
    {
        GameObject explosion = ObjectPool.instance.GetObject("Explosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
    }

}


public interface IParamOwner
{
    event Action<Parameter> onParamAttached;
    event Action<Parameter> onParamDetached;
    Dictionary<string, Parameter> parameters {get;}
    Parameter GetParam(string name);
}