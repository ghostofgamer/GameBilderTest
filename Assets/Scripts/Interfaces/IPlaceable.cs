using UnityEngine;

namespace Interfaces
{
   public interface IPlaceable
   {
      public bool CanPlaceOn(GameObject target);
   }
}