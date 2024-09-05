using Interfaces;
using UnityEngine;

public abstract class ItemMovement : MonoBehaviour,IItemMovable
{
    protected Item Item;

    private void Start()
    {
        Item = GetComponent<Item>();
    }

    public virtual void Move(RaycastHit hit, float offset, float currentRotation)
    {
        Item._isBildUpCube = false;
        Vector3 surfacePoint = hit.point;
        Vector3 surfaceNormal = hit.normal;
        Vector3 newPosition = surfacePoint + surfaceNormal * offset;

        if (Vector3.Dot(newPosition - surfacePoint, surfaceNormal) < 0)
        {
            newPosition = surfacePoint + surfaceNormal * Mathf.Abs(offset);
        }

        Item.transform.position = newPosition;
        Vector3 upDirection = surfaceNormal;
        Vector3 forwardDirection = Vector3.Cross(upDirection, Vector3.right);
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, upDirection);
        Item.transform.rotation = targetRotation;
        Item.transform.Rotate(Vector3.up, currentRotation, Space.Self);
        Item.SetCanPlaceValue(true);
        // Item.ActivateCanPlace();
    }
}