using System;
using UnityEngine;

public class EnemyIdleState : BaseEnemyAIState
{
    public EnemyIdleState(EnemyAI owner) : base(owner)
    {
    }

    public override void OnEnterState()
    {
        //Debug.Log("IDLE_STATE");
    }

    public override void OnExitState()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        if (_owner.Actor.CurrentWeapon == null)
        {
            return;
        }

        if (GetTarget() != null)
        {
            _owner.State.SetState(EnemyState.CHASE);
        }
    }

}

