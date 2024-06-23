using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    private const string ShaderName = "Shader Graphs/Dissolve";

    [SerializeField] private List<Renderer> _renderers;

    private float _dissolve = 1;
    private List<Material> _dissolveMaterials;
    private Coroutine _coroutine;

    public event Action<float> Dissolved;

    private void Start()
    {
        _dissolveMaterials = new List<Material>();

        Dictionary<Material, Material> checkedMaterials = new Dictionary<Material, Material>();

        foreach (Renderer renderer in _renderers)
        {
            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
            {
                if(renderer.materials[i].shader.name == ShaderName)
                {
                    if(checkedMaterials.ContainsKey(renderer.materials[i]))
                    {
                        renderer.materials[i] = checkedMaterials[renderer.materials[i]];
                    }
                    else
                    {
                        renderer.materials[i] = new Material(renderer.materials[i]);
                        renderer.materials[i].SetFloat("_dissolve", _dissolve);
                        _dissolveMaterials.Add(renderer.materials[i]);
                    }
                }
            }
        }
    }

    public void Show(float time)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Dissolving(0, time));
    }

    public void Hide(float time)
    {
        if( _coroutine != null )
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Dissolving(1, time));
    }

    private IEnumerator Dissolving(float target, float time)
    {
        yield return null;
        float current = 0;
        float startValue = _dissolve;

        while (current < time)
        {
            current = Mathf.Clamp(current + Time.deltaTime, 0, time);
            _dissolve = Mathf.Lerp(startValue, target, current / time);
            foreach (Material material in _dissolveMaterials)
                material.SetFloat("_dissolve", _dissolve);
            
            yield return null;
        }

        _dissolve = Mathf.Clamp01(_dissolve);
        Dissolved?.Invoke(_dissolve);
        _coroutine = null;
    }
}
