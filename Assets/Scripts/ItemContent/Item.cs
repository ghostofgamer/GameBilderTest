using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _layerValue;
    [SerializeField] private ItemMaterialChanger _itemMaterialChanger;
    
    private bool _isCanPlace;
    private bool _isInvalidLocation;
    
    
    public bool _isBildUpCube;


    public bool IsPermissionBuild { get; private set; }
    
    public bool IsBuildStage { get; private set; }

    private void Start()
    {
        _itemMaterialChanger = GetComponent<ItemMaterialChanger>();
    }

    private void Update()
    {
        if (!IsBuildStage)
            return;

        if (!_isCanPlace && !_isInvalidLocation)
        {
            NegativeBuildPlace();
            _itemMaterialChanger.SetMaterial(MaterialNames.Fade);
        }

        if (_isInvalidLocation)
        {
            _itemMaterialChanger.SetMaterial(MaterialNames.Negative);
            NegativeBuildPlace();
        }

        if (!_isInvalidLocation && _isCanPlace || _isCanPlace && _isBildUpCube)
        {
            _itemMaterialChanger.SetMaterial(MaterialNames.Positive);
            PositiveBuildPlace();
        }
    }

    public virtual void ActivateBildStage()
    {
        _itemMaterialChanger.SetMaterial(MaterialNames.Fade);
        IsBuildStage = true;
        gameObject.layer = 8;
    }

    public virtual void DeactivateBildStage()
    {
        _itemMaterialChanger.SetMaterial(MaterialNames.Normal);
        IsBuildStage = false;
        gameObject.layer = _layerValue;
        ReturnDefaultSettings();
    }

    public void ReturnDefaultSettings()
    {
        IsPermissionBuild = false;
        _isInvalidLocation = false;
        _isCanPlace = false;
        _isBildUpCube = false; 
    }
    
    private void NegativeBuildPlace()
    {
        IsPermissionBuild = false;
    }

    private void PositiveBuildPlace()
    {
        IsPermissionBuild = true;
    }
    
    public void SetCanPlaceValue(bool canPlace)
    {
        _isCanPlace = canPlace;
    }

    public void SetInvalidPosition(bool invalidPosition)
    {
        _isInvalidLocation = invalidPosition;
    }
}