using UnityEngine;

public class EnemyPathPoint : MonoBehaviour
{
    private Vector3 _position;

    public Vector3 Position => _position;

    private void Awake()
    {
        _position = transform.position;
    }
}
