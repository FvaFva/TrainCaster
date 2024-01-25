using UnityEngine;

[RequireComponent (typeof(Animator))]
public class EnemyModel : MonoBehaviour, IStored
{
    [SerializeField] private int _speed;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _hitPointsDice;

    private Transform _baseParent;
    private Vector3 _baseSize;
    private ICell<EnemyModel> _cell;

    private void Awake()
    {
        TryGetComponent<Animator>(out Animator animator);
        MainAnimator = animator;
        gameObject.SetActive(false);
        _baseParent = transform.parent;
        _baseSize = transform.localScale;
    }

    public Animator MainAnimator { get; private set; }
    public int Speed => _speed;

    public void ConnectToCell(ICell<IStored> myCell)
    {
        _cell = (ICell<EnemyModel>)myCell;
    }

    public void Initialized(EnemyRouter parent)
    {
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void TakeOff()
    {
        if(_baseParent != null)
            transform.parent = _baseParent;
        else 
            transform.parent = null;

        transform.localPosition = Vector3.zero;
        transform.rotation = default;
        transform.localScale = _baseSize;
        _cell.AddItem(this);
        gameObject.SetActive(false);
    }

    public int GetRandomizeHitPoints()
    {
        int hitPointsDiced = Random.Range(0, _hitPointsDice);
        int hitPoints = _hitPoints;

        if (Random.Range(0, 2) == 1)
            hitPoints += hitPointsDiced;
        else
            hitPoints -= hitPointsDiced;

        ScaleSizeToHitPoints(hitPoints);
        return hitPoints;
    }

    private void ScaleSizeToHitPoints(int hitPoints)
    {
        float sizeCoefficient = (float)_hitPoints / (float)hitPoints;
        Vector3 size = _baseSize * sizeCoefficient;
        transform.localScale = size;
    }
}
