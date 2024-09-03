using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetDefaultMaterial()
    {
        SetMaterial(_defaultMaterial);
    }

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
