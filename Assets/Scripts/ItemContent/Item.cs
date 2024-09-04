using System.Xml;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _fadeMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    protected bool IsBildStage;
    public bool IsPermissionBild;
    public bool _canTrigger;
    public bool _isCanPlace;
    public bool _isBildUpCube;


    private void Update()
    {
        if (!IsBildStage)
            return;

        if (!_isCanPlace && !_canTrigger)
        {
            DontBildPlace();
            SetFadeMaterial();
        }

        if (_canTrigger)
        {
            SetNegativeMaterial();
            DontBildPlace();
        }

        if (!_canTrigger && _isCanPlace || _isCanPlace && _isBildUpCube)
        {
            SetPositiveMaterial();
            PositivePlaceBild();
        }
    }

    public void ActivateCanPlace()
    {
        _isCanPlace = true;
    }

    public void DeactivateCanPlace()
    {
        _isCanPlace = false;
    }

    public void SetDefaultMaterial()
    {
        SetMaterial(_defaultMaterial);
    }

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }

    public void SetPositiveMaterial()
    {
        SetMaterial(_greenMaterial);
    }

    public void SetNegativeMaterial()
    {
        SetMaterial(_redMaterial);
    }

    public void SetFadeMaterial()
    {
        SetMaterial(_fadeMaterial);
    }

    public virtual void ActivateBildStage()
    {
        /*gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;*/
        SetMaterial(_fadeMaterial);
        IsBildStage = true;
    }

    public virtual void DeactivateBildStage()
    {
        /*gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;*/
        SetMaterial(_defaultMaterial);
        IsBildStage = false;
    }

    public void DontBildPlace()
    {
        /*
        if (_canTrigger)
            return;
            */

        IsPermissionBild = false;
    }

    public void PositivePlaceBild()
    {
        /*if (_canTrigger)
            return;*/

        IsPermissionBild = true;
    }
}