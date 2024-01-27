using UnityEngine;

[RequireComponent (typeof(Animator))]
public class EnemyView : MonoBehaviour, IStored
{
    [SerializeField] private int _speed;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _hitPointsDice;

    private Transform _baseParent;
    private Vector3 _baseSize;
    private ICell<EnemyView> _cell;

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
        _cell = (ICell<EnemyView>)myCell;
    }

    public void Activate(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(true);
    }

    public void TakeOff()
    {
        transform.parent = _baseParent;
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

        transform.localScale *= (float)_hitPoints / (float)hitPoints;
        return hitPoints;
    }
}
