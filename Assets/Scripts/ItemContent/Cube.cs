using System;
using UnityEngine;

public class Cube : Item, IPlaceable
{
    [SerializeField] private BoxCollider _boxCollider;
    
    public bool CanPlaceOn(GameObject target)
    {
        return target.layer == LayerMask.NameToLayer("Floor") || target.layer == LayerMask.NameToLayer("Cube");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsBildStage)
            return;

        if (_isBildUpCube)
            return;
        /*if (other.TryGetComponent(out Floor floor))
            return;*/

        if (other.TryGetComponent(out Item item)||other.TryGetComponent(out Wall wall))
        {
            _canTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsBildStage)
            return;
        
        if (_isBildUpCube)
            return;

        if (other.TryGetComponent(out Item item)||other.TryGetComponent(out Wall wall))
        {
            _canTrigger = false;
        }
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
