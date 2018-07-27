using System;
using UnityEngine;


    public class Timer
    {
        private readonly float _delay;
        private readonly float _period;
        private readonly float _totalTime;
        public float Delay { get { return _delay; } }

        private float _startTime;
        private float _periodTime;
        private float _finishTime;

        private Action _periodCallback;
        private Action _startCallback;
        private Action _finishCallback;


        public Timer(float totalTime_, float period_ = 0f, float delay_ = 0f)
        {
            _delay = delay_;
            _period = period_;
            _totalTime = totalTime_;
        }

        public Timer OnStart(Action startCallback_)
        {
            _startCallback = startCallback_;
            return this;
        }

        public Timer OnPeriod(Action periodCallback_)
        {
            _periodCallback = periodCallback_;
            return this;
        }

        public Timer OnComplete(Action finishCallback_)
        {
            _finishCallback = finishCallback_;
            return this;
        }

        public void Start()
        {
            _startTime = Time.time + _delay;
            _finishTime = _totalTime + Time.time;
            if (_period > 0) {
                _periodTime = _period + _startTime;
            }

        }


        public void Update()
        {
            if (_startTime > 0 && Time.time >= _startTime) {
                OnStart();
            }

            if (_periodTime > 0 && Time.time >= _periodTime) {
                OnPeriod();
            }
            
            if (_finishTime > 0 && Time.time >= _finishTime) {
                Complete();
            }
        }

        private void OnStart()
        {
            if (_startCallback != null)
            {
                _startCallback.Invoke();
            }

            _startTime = 0f;
        }

        private void OnPeriod()
        {
            if (_periodCallback != null){
                _periodCallback.Invoke();
            }

            if (_period > 0) {
                _periodTime = _period + Time.time;
            }
        }

        private void Complete()
        {
            _periodTime = 0f;
            _finishTime = 0f;

            if (_finishCallback != null) {
                _finishCallback.Invoke();
            }

        }


        public void Release ()
        {
            _periodCallback = null;
            _startCallback = null;
            _finishCallback = null;
        }
}
