using FSMContent.States;
using UnityEngine;

namespace FSMContent
{
    public class FSMInitializator : MonoBehaviour
    {
        [SerializeField] private PlayerDragger _playerDragger;
        [SerializeField] private float _reachDistance = 5f;
        [SerializeField] private LayerMask _ignoreLayers;
        [SerializeField] private float _reachBildDistance = 15f;
        
        private FSM _fsm;

        private void Start()
        {
            _fsm = new FSM();
            _fsm.AddState(new StateAction(_fsm, _playerDragger,_reachDistance));
            _fsm.AddState(new StateBuild(_fsm, _playerDragger,_ignoreLayers,_reachBildDistance));
            _fsm.SetState<StateAction>();
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}