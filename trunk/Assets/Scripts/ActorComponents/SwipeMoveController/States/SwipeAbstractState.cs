using UnityEngine;

namespace Assets.Scripts.ActorControllers.SwipeMoveController
{
    public abstract class SwipeAbstractState : IState
    {
        protected readonly SwipeMove _owner;
        public Vector3 targetPos { get; internal set; }

        public SwipeAbstractState(SwipeMove owner)
        {
            _owner = owner;
        }

        public abstract void Update();
        public abstract void OnEnterState();
        public abstract void OnExitState();
    }
}
