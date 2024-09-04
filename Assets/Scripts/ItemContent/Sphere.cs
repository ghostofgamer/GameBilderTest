using UnityEngine;

public class Sphere : Item,IPlaceable
{
    [SerializeField] private SphereCollider _sphereCollider;
    
    public bool CanPlaceOn(GameObject target)
    {
        return target.layer == LayerMask.NameToLayer("Wall");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!IsBildStage)
            return;
        
        if (other.TryGetComponent(out Item item)||other.TryGetComponent(out Floor floor))
        {
            _canTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsBildStage)
            return;
  

        if (other.TryGetComponent(out Item item)||other.TryGetComponent(out Floor floor))
        {
            _canTrigger = false;
        }
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
