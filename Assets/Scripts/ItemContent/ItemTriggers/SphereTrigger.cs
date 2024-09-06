using EnvironmentContent;
using UnityEngine;

namespace ItemTriggers
{
    public class SphereTrigger : ItemTrigger
    {
        protected override void CheckTrigger(Collider other, bool value)
        {
            if (!Item.IsBuildStage)
                return;

            if (other.TryGetComponent<Item>(out _) || other.TryGetComponent<Floor>(out _))
                Item.SetInvalidPosition(value);
        }
    }
}
