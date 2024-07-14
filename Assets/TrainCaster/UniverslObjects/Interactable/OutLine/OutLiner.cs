using System.Collections.Generic;
using UnityEngine;

public class OutLiner : MonoBehaviour
{
    [SerializeField] List<Material> _materials;

    private Renderer _current;
    private List<Material> _currentMaterials;
    private int _countMaterials;

    private void Awake()
    {
        _countMaterials = _materials.Count;
    }

    public void Show(Renderer renderer)
    {
        if ( _current != null )
            Hide();

        if (renderer == null)
            return;

        _current = renderer;
        int currentIndex = renderer.materials.Length;
        Material[] materials = new Material[currentIndex + _countMaterials];
        renderer.materials.CopyTo(materials, 0);
        _currentMaterials = new List<Material>();

        int j = currentIndex;

        foreach (Material mat in _materials)
        {
            materials[j] = mat;
            j++;
        }

       renderer.materials = materials;

        for ( int i = currentIndex; i < materials.Length; i++ )
            _currentMaterials.Add(renderer.materials[i]);
    }

    public void Hide()
    {
        if (_current == null)
            return;

        Material[] materials = new Material[_current.materials.Length - _countMaterials];
        int j = 0;

        foreach(Material mat in _current.materials)
        {
            if(_currentMaterials.Contains(mat) == false)
            {
                materials[j] = mat;
                j++;
            }
        }

        _current.materials = materials;
        _current = null;
    }
}
