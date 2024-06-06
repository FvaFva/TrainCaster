using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Player/Resource", order = 51)]
public class Currency : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private MinedCurrencyTransport _transportPrefab;

    public Sprite Icon => _icon;
    public MinedCurrencyTransport Transport => _transportPrefab;
    public string Name => _name;
    public string Description => _description;
    public Vector3 StoragePosition { get; private set; }

    public void InitiateStoragePosition(Vector3 position)
    {
        if (StoragePosition != Vector3.zero)
            return;

        StoragePosition = position;
    }
}