using Interfaces;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerDragger : MonoBehaviour
    {
        private const string MouseScrollWheel = "Mouse ScrollWheel";
        
        [SerializeField] private Transform _defaultPositionItem;
        [SerializeField] private float _offset = 0.1f;

        private float _currentRotation = 0f;
        private  float _step = 45;
        private  float _scroll;
        private IItemMovable _itemMovable;
        
        public Item Item { get; private set; }
    
        public void SetItem(Item item)
        {
            Item = item;
            _itemMovable = Item.GetComponent<IItemMovable>();
            Item.transform.position = _defaultPositionItem.position;
            Item.transform.parent = transform;
            Item.ActivateBildStage();
            _currentRotation = Item.transform.rotation.y;
        }

        public void Drag(RaycastHit hit)
        {
            _itemMovable.Move(hit, _offset, _currentRotation);
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
            _scroll = Input.GetAxis(MouseScrollWheel);

            if (_scroll > 0)
                _currentRotation += _step;
            else if (_scroll < 0)
                _currentRotation -= _step;
        }

        public void ReturnPosition()
        {
            Item.transform.position = _defaultPositionItem.position;
            Item.ReturnDefaultSettings();
        }
    }
}