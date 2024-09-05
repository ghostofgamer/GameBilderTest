using UnityEngine;

public class CubeTrigger : ItemTrigger
{
    protected override void CheckTrigger(Collider other, bool value)
    {
        if (!Item.IsBuildStage || Item._isBildUpCube)
            return;

        if (other.TryGetComponent<Item>(out _) || other.TryGetComponent<Wall>(out _))
            Item.SetInvalidPosition(value);
    }
}