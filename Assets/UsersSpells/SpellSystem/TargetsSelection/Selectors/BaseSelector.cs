using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseSelector : MonoBehaviour
{
    [SerializeField] private MeshRenderer _render;
    [SerializeField] private Color _wrongColor;

    private Color _goodColor;
    private Camera _camera;

    protected bool IsCorrect;
    protected bool IsMoving;
    protected Vector3 PointToMove;
    protected Transform CastPoint {  get; private set; }

    private void Awake()
    {
        _goodColor = _render.material.color;
        _camera = Camera.main;
    }

    private void Update()
    {
        ProcessSelection();

        if (IsMoving)
            transform.position = PointToMove;

        if (IsCorrect)
            _render.material.color = _goodColor;
        else
            _render.material.color = _wrongColor;
    }

    public void SetCastPoint(Transform point)
    {
        CastPoint = point;
    }

    public void Starting()
    {
        gameObject.SetActive(true);
    }

    public CastTarget Finishing()
    {
        gameObject.SetActive(false);
        return GetCurrentTarget();
    }

    protected abstract void ProcessSelection();
    protected abstract CastTarget GetCurrentTarget();

    protected bool TryGetCameraHits(out RaycastHit[] hits)
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray tapRay = _camera.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(tapRay, 100);
            return hits.Count() > 0;
        }
        else
        {
            hits = new RaycastHit[0];
            return false;
        }
    }

    protected IEnumerable<RaycastHit> GetTypedHits<HitType>(RaycastHit[] hits) where HitType : MonoBehaviour
    {
        return hits.Where(hit => hit.collider.ContainBehavior<HitType>());
    }
}