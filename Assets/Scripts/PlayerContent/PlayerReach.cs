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

    private float currentRotation = 0f;


    public void SetItem(Item item)
    {
        Item = item;
        Item.transform.position = _defaultPositionItem.position;
        Item.transform.parent = transform;
        Item.ActivateBildStage();
        currentRotation = Item.transform.rotation.y;
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

                            // Item.transform.position = surfacePoint + surfaceNormal * _offset;

                            // Item.transform.up = surfaceNormal;


                            /*// Выравниваем объект по нормали поверхности
                            Quaternion targetRotation = Quaternion.LookRotation(surfaceNormal);
                            Debug.Log("тапген " + targetRotation);
                            Item.transform.rotation = targetRotation;*/


                            // Ваша нормаль поверхности
                            Vector3 upDirection = surfaceNormal;
                            Vector3 forwardDirection =
                                Vector3.Cross(upDirection,
                                    Vector3.right); // Выбираем произвольное направление, перпендикулярное upDirection

                            Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, upDirection);
                            Debug.Log("тапген " + targetRotation);
                            Item.transform.rotation = targetRotation;


                            Item.transform.Rotate(Vector3.up, currentRotation, Space.Self);

                            Item.ActivateCanPlace();
                        }
                    }
                    else
                    {
                        Item._isBildUpCube = false;

                        Item.transform.position = _defaultPositionItem.position;
                        Item.DeactivateCanPlace();
                    }
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _reachBildDistance, Color.red);
                Item.transform.position = _defaultPositionItem.position;
                Item.DeactivateCanPlace();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Item != null && Item.IsPermissionBild)
                {
                    Item.transform.parent = null;
                    Item.DeactivateBildStage();
                    Item = null;
                }
            }
        }

        if (Item != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float step = 45;

            if (scroll > 0)
                currentRotation += step;
            else if (scroll < 0)
                currentRotation -= step;
        }
    }
}