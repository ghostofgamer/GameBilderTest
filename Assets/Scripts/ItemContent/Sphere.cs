using UnityEngine;

public class Sphere : Item, IPlaceable
{
    [SerializeField] private SphereCollider _sphereCollider;

    public bool CanPlaceOn(GameObject target)
    {
        return target.TryGetComponent<Wall>(out _);
    }

    public override void ActivateBildStage()
    {
        base.ActivateBildStage();
        _sphereCollider.isTrigger = true;
    }

    public override void DeactivateBildStage()
    {
        base.DeactivateBildStage();
        _sphereCollider.isTrigger = false;
    }
}