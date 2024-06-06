using UnityEngine;

[RequireComponent (typeof(Animator))]
public class EnemyView : MonoBehaviour, IStored
{
    [SerializeField] private int _speed;
    [SerializeField] private float _armor = 5;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _hitPointsDice;
    [SerializeField] private Dissolver _dissolver;
    [SerializeField] private int _rewardBasic;
    [SerializeField] private int _rewardDice;

    private Transform _baseParent;
    private Transform _transform;
    private Vector3 _baseSize;
    private float _animationSpeed;
    private Animator _animator;

    private void Awake()
    {
        TryGetComponent<Animator>(out Animator animator);
        _animator = animator;
        _transform = transform;
        _baseParent = _transform.parent;
        _baseSize = _transform.localScale;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _dissolver.Dissolved += OnDissolveFinished;
    }

    private void OnDisable()
    {
        _dissolver.Dissolved -= OnDissolveFinished;
    }

    public int Speed => _speed;
    public float Armor => _armor;
    public int RewardBasic => _rewardBasic;
    public int RewardDice => _rewardDice;

    public event System.Action<IStored> ReturnedToPool;

    public void Activate(Transform parent)
    {
        _transform.parent = parent;
        _transform.localPosition = Vector3.zero;
        _transform.localRotation = Quaternion.identity;
        _animator.speed = 1;
        gameObject.SetActive(true);
        _dissolver.Show(1f);
    }

    public void ChangeAnimationSpeed(float changeCoefficient = 0)
    {
        _animator.speed = _animationSpeed * (1 - changeCoefficient);
    }

    public void TakeOff()
    {
        _transform.parent = _baseParent;
        _transform.localScale = _baseSize;
        _animator.speed = 2;
        _animator.SetBool("Win", true);
        _dissolver.Hide(2f);
    }

    public int GetRandomizeHitPoints()
    {
        int hitPointsDiced = Random.Range(0, _hitPointsDice);
        int hitPoints = _hitPoints;

        if (Random.Range(0, 2) == 1)
            hitPoints += hitPointsDiced;
        else
            hitPoints -= hitPointsDiced;

        float coefficient = (float)hitPoints / (float)_hitPoints;
        _animationSpeed = 1 - (coefficient - 1);
        ChangeAnimationSpeed();
        _transform.localScale *= coefficient;
        return hitPoints;
    }

    private void OnDissolveFinished(float dissolve)
    {
        if(dissolve == 1)
        {
            ReturnedToPool?.Invoke(this);
            _animator.SetBool("Win", false);
            gameObject.SetActive(false);
        }
    }
}
