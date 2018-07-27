using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class CollisionImpact : MonoBehaviour
{
    public bool destroyOnApplied = true;
    public List<string> targetTags;
    public void Apply(EntityModel entity)
    {
        if (CheckTarget(entity)) {
            OnApply(entity);
        }
    }

    protected abstract void OnApply(EntityModel entity);

    protected virtual bool CheckTarget(EntityModel entity)
    {
        if (!targetTags.Contains(entity.type))
        {
            Debug.Log(string.Format("Can't apply {0} to {1}", this.ToString(), entity.type));
            return false;
        }
        return true;
    }

    protected virtual void OnAppliedComplete()
    {
        if (destroyOnApplied)
        {
            gameObject.SendMessageUpwards("DestroyOnApplyImpact", SendMessageOptions.DontRequireReceiver);
        }
    }
 }
