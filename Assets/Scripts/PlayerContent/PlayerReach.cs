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
        /*Item.GetComponent<Rigidbody>().isKinematic = true;
        Item.GetComponent<BoxCollider>().isTrigger = true;*/
        Item.transform.parent = transform;
        // Item.SetMaterial(_materialFade);
        Item.ActivateBildStage();
        
        
        
        /*Item.transform.localPosition = Vector3.zero; 
        Item.transform.localRotation = Quaternion.identity;*/
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
                    SetItem(item);
                    item.gameObject.layer = 8;
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

                if (hit.transform.TryGetComponent(out Ground ground) || hit.transform.TryGetComponent(out Item item))
                {
                    IPlaceable placeable = Item.GetComponent<IPlaceable>();

                    if (placeable != null && placeable.CanPlaceOn(hit.collider.gameObject))
                    {
                        if (Item.GetComponent<Cube>() && hit.collider.gameObject.GetComponent<Cube>())
                        {
                            Item._isBildUpCube = true;
                            Item.ActivateCanPlace();
                            
                            Item.transform.position = hit.collider.gameObject.transform.position;
                            float y = Item.transform.position.y + 3;
                            Item.transform.position = new Vector3(Item.transform.position.x, y, Item.transform.position.z);
                        }
                        else
                        {
                            Item._isBildUpCube = false;
                            
                            Vector3 surfacePoint = hit.point;
                            Vector3 surfaceNormal = hit.normal;
                            Item.transform.position = surfacePoint + surfaceNormal * _offset;
                            Item.transform.up = surfaceNormal;
                            Item.ActivateCanPlace();
                        }


                        /*Item.PositivePlaceBild();
                        Item.SetPositiveMaterial();*/
                    }
                    else
                    {
                        Item._isBildUpCube = false;
                        
                        Item.transform.position = _defaultPositionItem.position;
                        Item.DeactivateCanPlace();
                        /*Item.DontBildPlace();
                        Item.SetFadeMaterial();*/
                    }
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachBildDistance, Color.red);
                Item.transform.position = _defaultPositionItem.position;
                Item.DeactivateCanPlace();
                /*Item.DontBildPlace();
                Item.SetFadeMaterial();*/
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Item != null && Item.IsPermissionBild)
                {
                    /*Item.GetComponent<Rigidbody>().isKinematic = false;
                    Item.GetComponent<BoxCollider>().isTrigger = false;*/
                    Item.transform.parent = null;
                    // Item.SetDefaultMaterial();
                    Item.gameObject.layer = 0;
                    Item.DeactivateBildStage();
                    Item = null;
                }
            }
        }
    }
}