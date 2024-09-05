using UnityEngine;

public class Cube : Item, IPlaceable
{
    [SerializeField] private BoxCollider _boxCollider;

    public bool CanPlaceOn(GameObject target)
    {
        return target.TryGetComponent<Floor>(out _) || target.TryGetComponent<Cube>(out _);
    }
    
    public override void ActivateBildStage()
    {
        base.ActivateBildStage();
        _boxCollider.isTrigger = true;
    }

    public override void DeactivateBildStage()
    {
        base.DeactivateBildStage();
        _boxCollider.isTrigger = false;
    }
}