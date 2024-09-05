using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _layerValue;
    [SerializeField] private ItemMaterialChanger _itemMaterialChanger;

    private int _ignoreLayer = 8;
    
    public ItemMaterialChanger ItemMaterialChanger => _itemMaterialChanger;
    
    protected bool IsCanPlace{ get; private set; }
    
    protected bool IsInvalidLocation { get; private set; }
    
    public bool IsCanBuildTop { get; private set; }

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

        UpdateBuildStage();
    }

    private void UpdateBuildStage()
    {
        UpdateNegativeBuildStage();
        UpdatePositiveBuildStage();
    }

    protected virtual void UpdateNegativeBuildStage()
    {
        if (!IsCanPlace && !IsInvalidLocation)
        {
            NegativeBuildPlace();
            _itemMaterialChanger.SetMaterial(MaterialNames.Fade);
        }

        if (IsInvalidLocation)
        {
            _itemMaterialChanger.SetMaterial(MaterialNames.Negative);
            NegativeBuildPlace();
        }
    }

    protected virtual void UpdatePositiveBuildStage()
    {
        if (!IsInvalidLocation && IsCanPlace)
        {
            _itemMaterialChanger.SetMaterial(MaterialNames.Positive);
            PositiveBuildPlace();
        }
    }

    public virtual void ActivateBildStage()
    {
        _itemMaterialChanger.SetMaterial(MaterialNames.Fade);
        IsBuildStage = true;
        gameObject.layer = _ignoreLayer;
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
        IsInvalidLocation = false;
        IsCanPlace = false;
        IsCanBuildTop = false;
    }

    protected void NegativeBuildPlace()
    {
        IsPermissionBuild = false;
    }

    protected void PositiveBuildPlace()
    {
        IsPermissionBuild = true;
    }

    public void SetCanPlaceValue(bool canPlace)
    {
        IsCanPlace = canPlace;
    }

    public void SetInvalidPosition(bool invalidPosition)
    {
        IsInvalidLocation = invalidPosition;
    }

    public void SetValueTopPosition(bool valueTop)
    {
        IsCanBuildTop = valueTop;
    }
}