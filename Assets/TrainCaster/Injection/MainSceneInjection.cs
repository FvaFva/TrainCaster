using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInjection : MonoInstaller
{
    [Header("Inventory")]
    [SerializeField] private InventoryBinder _inventoryBinder;
    [SerializeField] private LootBoxUnpacker _boxUnpacker;
    [SerializeField] private SpellCrafter _spellCrafter;
    [SerializeField] private MainCardView _mainCardView;

    [Header("Cast")]
    [SerializeField] private SpellCaster _caster;
    [SerializeField] private TakerInteractive _taker;
    [SerializeField] private SpellBar _spellBar;
    [SerializeField] private List<BaseSpellEffect> _spellEffects;
    [SerializeField] private List<BaseEnemySelector> _additionalSelectors;

    [Header("Train")]
    [SerializeField] private Train _train;
    [SerializeField] private Railways _railways;

    [Header("System")]
    [SerializeField] private PoolService _poolService;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Base _base;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private CurrencyService _mineService;
    [SerializeField] private Shop _shop;
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private AxisRotator _axisRotator;
    [SerializeField] private DragContainer _dragView;

    public override void InstallBindings()
    {
        Bind();
        Inject();
    }

    private void Bind()
    {
        UserInput input = Container.Instantiate<UserInput>();
        input.Enable();
        Container.Bind<UserInput>().FromInstance(input).AsSingle().NonLazy();
        Container.Bind<PoolService>().FromInstance(_poolService).AsSingle().NonLazy();
        Container.Bind<Shop>().FromInstance(_shop).AsSingle().NonLazy();
        Container.Bind<PlayerWallet>().FromInstance(_playerWallet).AsSingle().NonLazy();
        Container.Bind<GameStateBuilder>().FromNew().AsSingle().NonLazy();
        Container.Bind<CurrencyService>().FromInstance(_mineService).AsSingle().NonLazy();
        Container.Bind<ActiveEnemies>().FromInstance(new ActiveEnemies(_enemyFactory)).AsSingle().NonLazy();
        Container.Bind<MainCardView>().FromInstance(_mainCardView).AsSingle().NonLazy();
        Container.Bind<DragContainer>().FromInstance(_dragView).AsSingle().NonLazy();

        InventoryBind<SpellElement>(_boxUnpacker);
        InventoryBind<CraftedSpell>(_spellCrafter);
    }

    private void InventoryBind<T>(IInventorySource source) where T : ICard
    {
        Inventory<T> inventory = new Inventory<T>(source, _mainCardView);
        Container.Bind<Inventory<T>>().FromInstance(inventory).AsSingle().NonLazy();
    }

    private void Inject()
    {
        Container.Inject(_caster);
        Container.Inject(_taker);
        Container.Inject(_train);
        Container.Inject(_railways);
        Container.Inject(_enemyFactory);
        Container.Inject(_gameStateMachine);
        Container.Inject(_base);
        Container.Inject(_spellBar);
        Container.Inject(_inventoryBinder);
        Container.Inject(_axisRotator);

        foreach (BaseSpellEffect effect in _spellEffects)
            Container.Inject(effect);

        foreach (BaseEnemySelector additionalSelector in _additionalSelectors)
            Container.Inject(additionalSelector);
    }
}