using UnityEngine;

public class PotentGameZone : MonoBehaviour
{
    private const float AxisRange = 2;

    [Header("RedactorSettings")]
    [SerializeField] private bool _showCenter;
    [SerializeField] private bool _showBox;
    [SerializeField] private bool _showAxis;
    [SerializeField] private Color _color = Color.red;

    [Header("GameSettings")]
    [SerializeField] private Vector2 _size;

    
    private float _xOffsite;
    private float _zOffsite;

    private void Awake()
    {
        Transform = base.transform;
        _xOffsite = _size.x / 2;
        _zOffsite = _size.y / 2;
    }

    public Vector3 Center => Transform.position;
    public Quaternion Direction => Transform.rotation;
    public Transform Transform { get; private set; }
    public Vector3 Size => _size;

    public Vector3 GetRandomPoint()
    {
        return new Vector3(Center.x + Random.Range(-_xOffsite, _xOffsite), Center.y, Center.z + Random.Range(-_zOffsite, _zOffsite));
    }

    public void Rename(string name)
    {
        gameObject.name = name;
    }

    public void LookAt(Vector3 position)
    {
        Transform.LookAt(position);
    }

    public Vector3[,] GetWorldLocation()
    {
        Vector3[,] positions = new Vector3[2, 2];

        float x = Center.x;
        float y = Center.y;
        float z = Center.z;

        positions[0, 0] = new Vector3(x - _xOffsite, y, z - _zOffsite);
        positions[0, 1] = new Vector3(x - _xOffsite, y, z + _zOffsite);
        positions[1, 0] = new Vector3(x + _xOffsite, y, z - _zOffsite);
        positions[1, 1] = new Vector3(x + _xOffsite, y, z + _zOffsite);

        return positions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        float xOffsite = _size.x / 2;
        float zOffsite = _size.y / 2;
        float y = base.transform.position.y;
        float z = base.transform.position.z;
        float x = base.transform.position.x;

        if (_showCenter)
            Gizmos.DrawWireSphere(base.transform.position, 0.3f);

        if (_showAxis)
        {
            Gizmos.DrawLine(new Vector3(x, y, z + zOffsite + AxisRange), new Vector3(x, y, z - zOffsite - AxisRange));
            Gizmos.DrawLine(new Vector3(x + xOffsite + AxisRange, y, z), new Vector3(x - xOffsite - AxisRange, y, z));
        }

        if(_showBox)
        {
            Gizmos.DrawLine(new Vector3(x + xOffsite, y, z + zOffsite), new Vector3(x + xOffsite, y, z - zOffsite));
            Gizmos.DrawLine(new Vector3(x + xOffsite, y, z + zOffsite), new Vector3(x - xOffsite, y, z + zOffsite));
            Gizmos.DrawLine(new Vector3(x - xOffsite, y, z - zOffsite), new Vector3(x + xOffsite, y, z - zOffsite));
            Gizmos.DrawLine(new Vector3(x - xOffsite, y, z - zOffsite), new Vector3(x - xOffsite, y, z + zOffsite));
        }
    }
}
