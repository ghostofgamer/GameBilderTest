using PlayerContent;
using UnityEngine;

namespace FSMContent.States
{
    public class StateAction : FSMState
    {
        private readonly float _reachDistance;
        
        private Ray _ray;
        private RaycastHit _hit;
        
        public StateAction(FSM fsm, PlayerDragger playerDragger, float reachDistance) : base(fsm, playerDragger)
        {
            _reachDistance = reachDistance;
        }

        public override void UpdateState()
        {
            if (PlayerDragger.Item == null)
            {
                _ray = new Ray(PlayerDragger.transform.position, PlayerDragger.transform.forward);

                if (Physics.Raycast(_ray, out _hit, _reachDistance))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_hit.transform == null)
                            return;

                        if (_hit.transform.TryGetComponent(out Item item))
                            PlayerDragger.SetItem(item);
                    }
                }
            }

            if (PlayerDragger.Item != null)
                Fsm.SetState<StateBuild>();
        }
    }
}