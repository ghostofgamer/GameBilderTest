using System;
using System.Collections.Generic;

public class FSM
{
    private FSMState _stateCurrent { get; set; }

    private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

    public void Update()
    {
        _stateCurrent?.Stay();
    }

    public void AddState(FSMState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSMState
    {
        var type = typeof(T);

        if (_stateCurrent != null && _stateCurrent.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            _stateCurrent?.Exit();
            _stateCurrent = newState;
            _stateCurrent.Enter();
        }
    }
}