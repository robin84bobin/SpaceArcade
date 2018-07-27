using System;
using UnityEngine;

public enum EffectType
{
    BLOOD,
    FIRE,
    POISON,
    HEAL
}

[Serializable]
public  class EffectInfo
{
    public EffectType effectType;
    public ParticleSystem particleSystem;
}

