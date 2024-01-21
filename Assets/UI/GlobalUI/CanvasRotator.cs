using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasRotator : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        if(TryGetComponent<Canvas>(out Canvas temp) == false)
            enabled = false;

        if(temp.renderMode != RenderMode.WorldSpace)
            enabled = false;

        if(temp.worldCamera != null)
            _camera = temp.worldCamera.transform;
        else
            _camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = _camera.rotation;
    }
}
