using UnityEngine;

namespace Assets.Scripts.ActorControllers.SwipeMoveController
{
    /// <summary>
    /// state if there is touched and need moving
    /// </summary>
    class SwipeTouchedState : SwipeAbstractState
    {
        public float swipeThreshold { get; internal set; }
        public float moveThreshold { get; internal set; }
        public float straightness { get; internal set; }

        public SwipeTouchedState(SwipeMove owner) : base(owner)
        {
        }

        public override void OnEnterState()
        {
            _startMousPos = _owner.touchPosition;
            _startScreenPos = Camera.main.WorldToScreenPoint(_owner.transform.position);
           // Debug.Log("Swipe TOUCHED state enter");
        }

        public override void OnExitState()
        {
            //Debug.Log("Swipe TOUCHED state exit");
        }


        public override void Update()
        {
            //Debug.Log("touched Update()"); 
            if (_owner.touched)
            {
                //Debug.Log("dbg_TOUCH owner.touched");
                CalcTargetPos();
                if (delta.magnitude > swipeThreshold)
                {
                    MoveToTargetPos();
                }
            }
            else
            {
                _owner.State.SetState(SwipeState.UNTOUCHED).targetPos = targetPos;
                //Debug.Log("untouched target pos: " + targetPos.ToString());
            }
        }

        private Vector3 delta;
        private Vector3 _startMousPos;
        private Vector3 _startScreenPos;
        private Vector3 currScreenPos;
        private bool inTargetPos;

        private void CalcTargetPos()
        {
            delta = _owner.touchPosition - _startMousPos;
            
            switch (_owner.mode)
            {
                case SwipeRestriction.VERTICAL:
                    delta.x = 0f;
                    break;
                case SwipeRestriction.HORIZONTAL:
                    delta.y = 0f;
                    break;
            }

            currScreenPos = _startScreenPos + delta;
            if (_owner.CheckBounds(currScreenPos))
            {
                targetPos = Camera.main.ScreenToWorldPoint(currScreenPos);
            }

            //Debug.Log("dbg_TOUCH touchPos:" + _owner.touchPosition.ToString() + " startMousPos:" + _startMousPos.ToString() + " delta:" + delta.ToString());
            //Debug.Log("dbg_TOUCH currScreenPos:" + currScreenPos.ToString());
        }

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
            
        }
    }
}
