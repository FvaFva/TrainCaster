using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _highlightEffect;

    public void ChangeHighlight(bool highlight)
    {
        if (highlight)
            _highlightEffect.Play();
        else
            _highlightEffect.Pause();
    }
}
