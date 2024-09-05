using UnityEngine;

public class CubeMovement : ItemMovement
{
    public override void Move(RaycastHit hit, float offset, float currentRotation)
    {
        if (hit.collider.gameObject.TryGetComponent<Cube>(out _))
        {
            Item._isBildUpCube = true;
            // Item.ActivateCanPlace();
            Item.SetCanPlaceValue(true);
            Item.transform.position = hit.collider.gameObject.transform.position;
            Item.transform.rotation = hit.collider.gameObject.transform.rotation;
            float y = Item.transform.position.y + 3;
            Item.transform.position = new Vector3(Item.transform.position.x, y, Item.transform.position.z);
        }
        else
        {
            base.Move(hit, offset, currentRotation);
        }
    }
}