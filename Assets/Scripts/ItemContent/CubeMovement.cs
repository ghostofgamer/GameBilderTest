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
            transform.position = hit.collider.gameObject.transform.position;
            transform.rotation = hit.collider.gameObject.transform.rotation;
            _yPosition = transform.position.y + _offset;
            transform.position = new Vector3(transform.position.x, _yPosition, transform.position.z);
        }
        else
        {
            if (Item.IsCanBuildTop)
                Item.SetValueTopPosition(false);

            base.Move(hit, offset, currentRotation);
        }
    }
}