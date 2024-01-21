using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GenerateEnemy : MonoBehaviour
{
    [SerializeField] private EnemyFactory _factory;

    private Button _generateButton;

    private void Awake()
    {
        TryGetComponent<Button>(out _generateButton);
    }

    private void OnEnable()
    {
        _generateButton.onClick.AddListener(Generate);
    }

    private void OnDisable()
    {
        _generateButton.onClick.RemoveListener(Generate);
    }

    private void Generate()
    {
        _factory.CreateEnemy();
    }
}
