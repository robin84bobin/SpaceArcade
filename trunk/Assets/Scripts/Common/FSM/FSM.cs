using System.Collections.Generic;


public class FSM <TKey,TState> where TState:IState
{
    private Dictionary<TKey,TState> _states;

    private TState _currentState;
    public TState CurrentState {
        get { return _currentState;}
    }

    public FSM()
    {
        _states = new Dictionary<TKey, TState> ();
    }

    public void Add(TKey key_,TState state_)
    {
        if (!_states.ContainsKey (key_)) {
            _states.Add (key_, state_);
        }
    }

    public void Remove(TKey key_)
    {
        _states.Remove (key_);
    }

    public TState SetState(TKey key_)
    {
        if (_currentState != null)
        {
            if (_currentState.Equals(_states[key_]))
            {
                return _currentState;
            }
            _currentState.OnExitState();
        }

        _currentState = _states [key_];
        _currentState.OnEnterState ();
        return _currentState;
    }
}



