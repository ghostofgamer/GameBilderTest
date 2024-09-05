using UnityEngine;

public class StateBuild : FSMState
{
    private readonly LayerMask _ignoreLayers;
    private readonly float _reachBildDistance;

    public override void Enter()
    {
        Debug.Log("Enter state Bild");
    }

    public override void Exit()
    {
        Debug.Log("Exit state Bild");
    }

    public override void UpdatePosition()
    {
        if (PlayerDragger.Item != null)
        {
            Ray ray = new Ray(PlayerDragger.transform.position, PlayerDragger.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _reachBildDistance, ~_ignoreLayers))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                if (hit.transform.TryGetComponent(out Ground ground) || hit.transform.TryGetComponent(out Item item))
                {
                    IPlaceable placeable = PlayerDragger.Item.GetComponent<IPlaceable>();

                    if (placeable != null && placeable.CanPlaceOn(hit.collider.gameObject))
                        PlayerDragger.Drag(hit);
                    else
                        PlayerDragger.ReturnPosition();
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachBildDistance, Color.red);
                PlayerDragger.ReturnPosition();
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerDragger.Drop();
            }

            if (PlayerDragger.Item == null)
            {
                Fsm.SetState<StateAction>();
            }
        }

        if (PlayerDragger.Item != null)
        {
            PlayerDragger.ItemRotate();
        }
    }

    public StateBuild(FSM fsm, PlayerDragger playerDragger, LayerMask ignoreLayers, float reachBildDistance) : base(fsm,
        playerDragger)
    {
        _ignoreLayers = ignoreLayers;
        _reachBildDistance = reachBildDistance;
    }
}