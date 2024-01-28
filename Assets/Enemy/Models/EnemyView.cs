using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class EnemyView : MonoBehaviour, IStored
{
    [SerializeField] private int _speed;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _hitPointsDice;
    [SerializeField] private Dissolver _dissolver;

    private Transform _baseParent;
    private Vector3 _baseSize;
    private ICell<EnemyView> _cell;

    private void Awake()
    {
        TryGetComponent<Animator>(out Animator animator);
        MainAnimator = animator;
        _baseParent = transform.parent;
        _baseSize = transform.localScale;
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
        _dissolver.Show(1f);
    }

    public void TakeOff()
    {
        transform.parent = _baseParent;
        transform.localScale = _baseSize;
        MainAnimator.SetBool("Win", true);
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

        transform.localScale *= (float)_hitPoints / (float)hitPoints;
        return hitPoints;
    }

    private void OnDissolveFinished(float dissolve)
    {
        if(dissolve == 1)
        {
            _cell.AddItem(this);
            MainAnimator.SetBool("Win", false);
            gameObject.SetActive(false);
        }
    }
}
