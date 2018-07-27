using System;
using UnityEngine;

public class EnemyChaseState : BaseEnemyAIState
{
    private GameObject _target;
    private Vector3 _weaponDelta;

    public EnemyChaseState(EnemyAI owner) : base(owner)
    {
    }

    public override void OnEnterState()
    {
        //Debug.Log("CHASE_STATE");
    }

    public override void OnExitState()
    {
        //throw new NotImplementedException();
    }


    public override void Update()
    {
        if (_owner.Actor.CurrentWeapon == null)
        {
            _owner.State.SetState(EnemyState.IDLE);
            return;
        }

        _target = GetTarget();
        if (_target != null)
        {
            _weaponDelta = _owner.Actor.transform.position - _owner.Actor.CurrentWeapon.projectileSpawnPoint.position;
            Vector3 targetPos = new Vector3(_owner.transform.position.x, _target.transform.position.y + _weaponDelta.y, _owner.transform.position.z);
            _owner.transform.position = Vector3.Lerp(_owner.transform.position, targetPos, Time.deltaTime);
        }
        else
        {
            _owner.State.SetState(EnemyState.IDLE);
        }
    }
}

