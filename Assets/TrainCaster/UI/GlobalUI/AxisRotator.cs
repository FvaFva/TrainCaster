using UnityEngine;
using Zenject;

public class AxisRotator : MonoBehaviour
{
    private const int ScreenSeparator = 2;
    private const int Minus = -1;
    private const int Plus = 1;


    [SerializeField] private float _maxAngle = 15f;

    [Inject] private UserInput _input;

    private Transform _transform;
    private Vector2 _screenCentre;
    private float _inverseCenterX;
    private float _inverseCenterY;

    private void Awake()
    {
        _transform = transform;
        _screenCentre = new Vector2(Screen.width / ScreenSeparator, Screen.height / ScreenSeparator);
        _inverseCenterX = 1f / _screenCentre.x;
        _inverseCenterY = 1f / _screenCentre.y;
    }

    private void Update()
    {
        Vector2 offset = _input.SystemSource.CoursorPosition.ReadValue<Vector2>() - _screenCentre;
        _transform.rotation = Quaternion.Euler(-1 * Calculate(offset.y, _inverseCenterY), Calculate(offset.x, _inverseCenterX), 0f);
    }

    private float Calculate(float offsetValue, float centerValue)
    {
        return Mathf.Clamp(offsetValue * centerValue, Minus, Plus) * _maxAngle;
    }
}
