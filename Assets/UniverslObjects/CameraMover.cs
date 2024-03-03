using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Rotate(Vector3 rotation)
    {
        _camera.transform.Rotate(rotation);
    }
}