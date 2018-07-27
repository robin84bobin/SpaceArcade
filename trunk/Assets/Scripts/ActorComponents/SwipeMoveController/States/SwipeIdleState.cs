using UnityEngine;

namespace Assets.Scripts.ActorControllers.SwipeMoveController
{
    /// <summary>
    /// state if there is no touch and no moving
    /// </summary>
    /// 
    class SwipeIdleState : SwipeAbstractState
    {
        public SwipeIdleState(SwipeMove owner) : base(owner)
        {

        }

        public override void OnEnterState()
        {
            //Debug.Log("Swipe IDLE state enter");
        }

        public override void OnExitState()
        {
           // Debug.Log("Swipe IDLE state exit");
        }

        public override void Update()
        {
            if (_owner.touched)
            {
                _owner.State.SetState(SwipeState.TOUCHED);
            }
        }
    }
}
