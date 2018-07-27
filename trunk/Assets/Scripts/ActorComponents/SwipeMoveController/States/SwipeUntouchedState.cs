using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ActorControllers.SwipeMoveController
{
    /// <summary>
    ///  state if there is no touch already but need to move to target point
    /// </summary>
    class SwipeUntouchedState : SwipeAbstractState
    {
        public float moveThreshold { get; internal set; }
        public float straightness { get; internal set; }

        public SwipeUntouchedState(SwipeMove owner) : base(owner)
        {
        }

        public override void OnEnterState()
        {
           // Debug.Log("Swipe UNTOUCHED state enter");
        }

        public override void OnExitState()
        {
           // Debug.Log("Swipe UNTOUCHED state exit");
        }

        public override void Update()
        {
            MoveToTargetPos();
            if (_owner.touched)
            {
                _owner.State.SetState(SwipeState.TOUCHED);
            }
        }

        private bool inTargetPos;

        internal void MoveToTargetPos()
        {
            if ((_owner.transform.position - targetPos).magnitude < moveThreshold)
            {
                CommandManager.Execute(new MoveCommand(_owner.transform, targetPos));
            }

            inTargetPos = targetPos == _owner.transform.position;

            if (!inTargetPos)
            {
                Vector3 newPos = Vector3.Lerp(_owner.transform.position, targetPos, straightness);
                CommandManager.Execute(new MoveCommand(_owner.transform, newPos));
            }
            else
            {
                _owner.State.SetState(SwipeState.IDLE);
            }
        }
    }
}
