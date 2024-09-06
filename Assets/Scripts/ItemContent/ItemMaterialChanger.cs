using Enums;
using UnityEngine;

public class ItemMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _fadeMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetMaterial(MaterialNames materials)
    {
        switch (materials)
        {
            case MaterialNames.Normal:
                _meshRenderer.material = _defaultMaterial;
                break;
            case MaterialNames.Positive:
                _meshRenderer.material = _greenMaterial;
                break;
            case MaterialNames.Negative:
                _meshRenderer.material = _redMaterial;
                break;
            case MaterialNames.Fade:
                _meshRenderer.material = _fadeMaterial;
                break;
        }
    }
}
