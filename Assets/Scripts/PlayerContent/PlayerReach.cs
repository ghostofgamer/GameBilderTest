using UnityEngine;

public class PlayerReach : MonoBehaviour
{
    [SerializeField] private float _reachDistance = 5f;

    [SerializeField] private float _reachBildDistance = 15f;
    [SerializeField] private float _offset = 0.1f;
    [SerializeField] private LayerMask _ignoreLayers;
    [SerializeField] private Transform _defaultPositionItem;
    [SerializeField] private Material _materialFade;

    public Item Item { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
        Item.transform.position = _defaultPositionItem.position;
        Item.GetComponent<Rigidbody>().isKinematic = true;
        Item.transform.parent = transform;
        Item.SetMaterial(_materialFade);
        /*
        _item.transform.localPosition = Vector3.zero; 
        _item.transform.localRotation = Quaternion.identity;*/
    }

    private void Update()
    {
        if (Item == null)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _reachDistance))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachDistance, Color.red);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform == null)
                    return;

                if (hit.transform.TryGetComponent(out Item item))
                {
                    Debug.Log("ВЕЩЬ ");
                    SetItem(item);
                }
            }
        }

        if (Item != null)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, _reachBildDistance, ~_ignoreLayers))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                if (hit.transform.TryGetComponent(out Ground ground))
                {
                    Vector3 surfacePoint = hit.point;
                    Vector3 surfaceNormal = hit.normal;
                    Item.transform.position = surfacePoint + surfaceNormal * _offset;
                    Item.transform.up = surfaceNormal;
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachBildDistance, Color.red);
                Item.transform.position = _defaultPositionItem.position;
            }
            
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
        }
    }

    public bool IsRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit, _reachDistance);
    }
}