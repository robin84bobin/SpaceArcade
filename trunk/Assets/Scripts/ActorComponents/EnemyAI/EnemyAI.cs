using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum EnemyState
{
    IDLE,
    CHASE
}

public class EnemyAI : MonoBehaviour {

    public float fovAngle;
    public float distance = 20f;

    private EnemyChaseState _chaseState;
    private EnemyIdleState _idleState;

    public FSM <EnemyState,BaseEnemyAIState> State { get; private set; }

    public ActorController Actor { get; private set; }
    public Radar Radar { get; private set; }

    // Use this for initialization
    void Start() {

        Actor = GetComponent<ActorController>();

        Radar = gameObject.AddComponent<Radar>();
        Radar.distance = distance;
        Radar.fovAngle = fovAngle;
        Radar.targetTags = new List<string>();
        Radar.targetTags.Add("Player");

        InitStates();
	}

    private void InitStates()
    {
        State = new FSM<EnemyState, BaseEnemyAIState>();
        _idleState = new EnemyIdleState(this);
        _chaseState = new EnemyChaseState(this);
        State.Add(EnemyState.IDLE, _idleState);
        State.Add(EnemyState.CHASE, _chaseState);
        State.SetState(EnemyState.IDLE);
    }

    // Update is called once per frame
    void Update ()
    {
        State.CurrentState.Update();
        AutoAttack();
    }

    private RaycastHit _hit = new RaycastHit();
    private Vector3 _fwd;

    private void AutoAttack()
    {
        if (Actor.CurrentWeapon == null)
        {
            return;
        }

        _fwd = Actor.CurrentWeapon.projectileSpawnPoint.TransformDirection(Vector3.forward);
#if RAYCAST_DEBUG
        Debug.DrawRay(Actor.CurrentWeapon.projectileSpawnPoint.position, _fwd , Color.red);
#endif
        if (Physics.Raycast(Actor.CurrentWeapon.projectileSpawnPoint.position, _fwd, out _hit, distance))
        {
            if (_hit.collider.CompareTag("Player"))
            {
                Actor.actorModel.Attack();
            }
        }
    }
}
