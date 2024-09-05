using Interfaces;
using UnityEngine;

public class PlayerDragger : MonoBehaviour
{
    [SerializeField] private Transform _defaultPositionItem;
    [SerializeField] private float _offset = 0.1f;

    private IItemMovable ItemMovable { get; set; }
    private float currentRotation = 0f;
    
    public Item Item { get; private set; }
    
    public void SetItem(Item item)
    {
        Item = item;
        ItemMovable = Item.GetComponent<IItemMovable>();
        Item.transform.position = _defaultPositionItem.position;
        Item.transform.parent = transform;
        Item.ActivateBildStage();
        currentRotation = Item.transform.rotation.y;
    }

    public void Drag(RaycastHit hit)
    {
        ItemMovable.Move(hit, _offset, currentRotation);
    }

    public void Drop()
    {
        if (Item != null && Item.IsPermissionBuild)
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
        // Item.DeactivateCanPlace();
        
        // Item.SetCanPlaceValue(false);
        
        Item.ReturnDefaultSettings();
    }

    public void SetBoolItem()
    {
        Item._isBildUpCube = false;
    }
}