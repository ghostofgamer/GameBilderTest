using UnityEngine;

public class StateAction : FSMState
{
    private readonly float _reachDistance;

    public override void Enter()
    {
        Debug.Log("Enter state Action");
    }

    public override void Exit()
    {
        Debug.Log("Exit state Action");
       
    }

    public override void UpdatePosition()
    {
        Debug.Log("Update state Action");

        if (PlayerDragger.Item == null)
        {
            Ray ray = new Ray(PlayerDragger.transform.position, PlayerDragger.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, _reachDistance))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachDistance, Color.red);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform == null)
                    return;

                if (hit.transform.TryGetComponent(out Item item))
                {
                    PlayerDragger.SetItem(item);
                }
            }
        }

        if (PlayerDragger.Item != null)
        {
            Fsm.SetState<StateBuild>( );
        }
    }

    public StateAction(FSM fsm, PlayerDragger playerDragger,float reachDistance) : base(fsm, playerDragger)
    {
        _reachDistance = reachDistance;
    }
}
