using UnityEngine;

namespace Interfaces
{
    public interface  IItemMovable
    {
        public void Move(RaycastHit hit, float offset, float currentRotation);
    }
}