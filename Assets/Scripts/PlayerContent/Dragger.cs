using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Transform _defaultPositionItem;
    [SerializeField] private Material _materialFade;
    
    public Item Item { get; private set; }

    /*public void SetItem(Item item)
    {
        Item = item;
        Item.transform.position = _defaultPositionItem.position;
        Item.GetComponent<Rigidbody>().isKinematic = true;
        Item.transform.parent = transform;
        Item.SetMaterial(_materialFade);
        /*
        _item.transform.localPosition = Vector3.zero; 
        _item.transform.localRotation = Quaternion.identity;#1#
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Item != null)
            {
                Item.GetComponent<Rigidbody>().isKinematic = false;
                Item.transform.parent = null;
                Item.SetDefaultMaterial();
                Item = null;
            }
        }
    }*/
}