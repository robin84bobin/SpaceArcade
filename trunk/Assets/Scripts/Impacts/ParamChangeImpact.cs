
using System;
using System.Collections.Generic;
using UnityEngine;

public class ParamChangeImpact : CollisionImpact {

    public List<ParamInfo> parameters;

    protected override void OnApply(EntityModel entity)
    { 
        ProcessSkillModification();
        OnAppliedComplete();
	}

    ActorModel targetActor;
    private void ProcessSkillModification()
    {
        parameters.ForEach(
            (s) => {
                Parameter skill = targetActor.GetParam(s.paramName);
                if (skill != null) skill.ChangeValue(s.modifyValue);
            }
         );
    }

    protected override bool CheckTarget(EntityModel entity)
    {
        bool baseCheck = base.CheckTarget(entity);

        targetActor = entity as ActorModel;
        if (targetActor == null)
        {
            Debug.Log(string.Format("Can't apply {0} to {1}", this.ToString(), entity.ToString()));
            return false;
        }

        return true && baseCheck;
    }
}
