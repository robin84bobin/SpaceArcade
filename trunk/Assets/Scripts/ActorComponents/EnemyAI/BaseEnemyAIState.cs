
using UnityEngine;

public abstract class BaseEnemyAIState : IState
{
    protected EnemyAI _owner;

    public BaseEnemyAIState(EnemyAI owner)
    {
        _owner = owner;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void Update();

    protected GameObject GetTarget()
    {
        return _owner.Radar.target;
    }
}

