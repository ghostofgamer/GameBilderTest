using UnityEngine;

public class CubeMovement : ItemMovement
{
    private int _offset = 3;
    private float _yPosition;
    
    public override void Move(RaycastHit hit, float offset, float currentRotation)
    {
        if (hit.collider.gameObject.TryGetComponent<Cube>(out _))
        {
            if (!Item.IsCanBuildTop)
                Item.SetValueTopPosition(true);

            Item.SetCanPlaceValue(true);
            Item.transform.position = hit.collider.gameObject.transform.position;
            Item.transform.rotation = hit.collider.gameObject.transform.rotation;
            _yPosition = Item.transform.position.y + _offset;
            Item.transform.position = new Vector3(Item.transform.position.x, _yPosition, Item.transform.position.z);
        }
        else
        {
            if (Item.IsCanBuildTop)
                Item.SetValueTopPosition(false);

            base.Move(hit, offset, currentRotation);
        }
    }
}