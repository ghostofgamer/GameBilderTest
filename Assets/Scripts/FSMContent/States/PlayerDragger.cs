using UnityEngine;

public class PlayerDragger : MonoBehaviour
{
    [SerializeField] private Transform _defaultPositionItem;
    [SerializeField] private float _offset = 0.1f;

    public Item Item { get; private set; }
    private float currentRotation = 0f;


    public void SetItem(Item item)
    {
        Item = item;
        Item.transform.position = _defaultPositionItem.position;
        Item.transform.parent = transform;
        Item.ActivateBildStage();
        currentRotation = Item.transform.rotation.y;
    }

    public void Drag(RaycastHit hit)
    {
        if (Item.GetComponent<Cube>() && hit.collider.gameObject.GetComponent<Cube>())
        {
            Item._isBildUpCube = true;
            Item.ActivateCanPlace();

            Item.transform.position = hit.collider.gameObject.transform.position;
            Item.transform.rotation = hit.collider.gameObject.transform.rotation;
            float y = Item.transform.position.y + 3;
            Item.transform.position =
                new Vector3(Item.transform.position.x, y, Item.transform.position.z);
        }
        else
        {
            Item._isBildUpCube = false;

            Vector3 surfacePoint = hit.point;
            Vector3 surfaceNormal = hit.normal;

            Vector3 newPosition = surfacePoint + surfaceNormal * _offset;

            if (Vector3.Dot(newPosition - surfacePoint, surfaceNormal) < 0)
            {
                newPosition = surfacePoint + surfaceNormal * Mathf.Abs(_offset);
            }

            Item.transform.position = newPosition;

            Vector3 upDirection = surfaceNormal;
            Vector3 forwardDirection =
                Vector3.Cross(upDirection,
                    Vector3.right);

            Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, upDirection);

            Item.transform.rotation = targetRotation;


            Item.transform.Rotate(Vector3.up, currentRotation, Space.Self);

            Item.ActivateCanPlace();
        }
    }

    public void Drop()
    {
        if (Item != null && Item.IsPermissionBild)
        {
            Item.transform.parent = null;
            Item.DeactivateBildStage();
            Item = null;
        }
    }

    public void ItemRotate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float step = 45;

        if (scroll > 0)
            currentRotation += step;
        else if (scroll < 0)
            currentRotation -= step;
    }

    public void ReturnPosition()
    {
        Item.transform.position = _defaultPositionItem.position;
        Item.DeactivateCanPlace();
    }

    public void SetBoolItem()
    {
        Item._isBildUpCube = false;
    }
}