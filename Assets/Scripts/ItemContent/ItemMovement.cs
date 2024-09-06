using Interfaces;
using UnityEngine;

public abstract class ItemMovement : MonoBehaviour,IItemMovable
{
    protected Item Item;

    private Vector3 _surfacePoint;
    private Vector3 _surfaceNormal;
    private Vector3 _newPosition;
    private Vector3 _upDirection;
    private Vector3 _forwardDirection;
    private Quaternion _targetRotation;
    
    private void Start()
    {
        Item = GetComponent<Item>();
    }

    public virtual void Move(RaycastHit hit, float offset, float currentRotation)
    {
        _surfacePoint = hit.point;
        _surfaceNormal = hit.normal;
        _newPosition = _surfacePoint + _surfaceNormal * offset;

        if (Vector3.Dot(_newPosition - _surfacePoint, _surfaceNormal) < 0)
            _newPosition = _surfacePoint + _surfaceNormal * Mathf.Abs(offset);

        Item.transform.position = _newPosition;
        _upDirection = _surfaceNormal;
        _forwardDirection = Vector3.Cross(_upDirection, Vector3.right);
        _targetRotation = Quaternion.LookRotation(_forwardDirection, _upDirection);
        Item.transform.rotation = _targetRotation;
        Item.transform.Rotate(Vector3.up, currentRotation, Space.Self);
        Item.SetCanPlaceValue(true);
    }
}