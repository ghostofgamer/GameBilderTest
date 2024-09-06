using EnvironmentContent;
using Interfaces;
using PlayerContent;
using UnityEngine;

namespace FSMContent.States
{
    public class StateBuild : FSMState
    {
        private readonly LayerMask _ignoreLayers;
        private readonly float _reachBildDistance;

        private Ray _ray;
        private RaycastHit _hit;
        private IPlaceable _placeable;

        public StateBuild(FSM fsm, PlayerDragger playerDragger, LayerMask ignoreLayers, float reachBildDistance) : base(
            fsm,
            playerDragger)
        {
            _ignoreLayers = ignoreLayers;
            _reachBildDistance = reachBildDistance;
        }

        public override void UpdateState()
        {
            if (PlayerDragger.Item != null)
            {
                _ray = new Ray(PlayerDragger.transform.position, PlayerDragger.transform.forward);

                if (Physics.Raycast(_ray, out _hit, _reachBildDistance, ~_ignoreLayers))
                {
                    if (_hit.transform.TryGetComponent<Environment>(out _) || _hit.transform.TryGetComponent<Item>(out _))
                    {
                        _placeable = PlayerDragger.Item.GetComponent<IPlaceable>();

                        if (_placeable != null && _placeable.CanPlaceOn(_hit.collider.gameObject))
                            PlayerDragger.Drag(_hit);
                        else
                            PlayerDragger.ReturnPosition();
                    }
                }
                else
                {
                    PlayerDragger.ReturnPosition();
                }

                if (Input.GetMouseButtonDown(0))
                    PlayerDragger.Drop();

                if (PlayerDragger.Item == null)
                    Fsm.SetState<StateAction>();
            }

            if (PlayerDragger.Item != null)
                PlayerDragger.ItemRotate();
        }
    }
}