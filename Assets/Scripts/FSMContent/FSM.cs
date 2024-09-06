using System;
using System.Collections.Generic;

namespace FSMContent
{
    public class FSM
    {
        private FSMState _stateCurrent;
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public void Update()
        {
            _stateCurrent?.UpdateState();
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
                _stateCurrent = newState;
        }
    }
}