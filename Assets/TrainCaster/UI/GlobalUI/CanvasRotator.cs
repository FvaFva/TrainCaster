using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasRotator : MonoBehaviour
{
    [SerializeField] private Transform _customFocus;

    private Transform _focus;

    private void Awake()
    {
        if(TryGetComponent<Canvas>(out Canvas temp) == false)
            enabled = false;
        else if(temp.renderMode != RenderMode.WorldSpace)
            enabled = false;
        else if(_customFocus != null)
            _focus = _customFocus;
        else if (temp.worldCamera != null)
            _focus = temp.worldCamera.transform;
        else
            _focus = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = _focus.rotation;
    }
}
