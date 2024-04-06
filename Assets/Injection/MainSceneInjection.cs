using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInjection : MonoInstaller
{
    [SerializeField] private SpellCaster _caster;
    [SerializeField] private TakerInteractive _taker; 
    [SerializeField] private LootBoxUnpacker _boxUnpacker;
    [SerializeField] private SpellInventoryView _inventoryView;
    [SerializeField] private Train _train;
    [SerializeField] private Railways _railways;
    [SerializeField] private SpellBar _spellBar;
    [SerializeField] private PoolService _poolService;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Base _base;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private List<BaseSpellEffect> _spellEffects;
    [SerializeField] private List<BaseAdditionalEnemySelector> _additionalSelectors;

    public override void InstallBindings()
    {
        Bind();
        Inject();
        Init();
    }

    private void Init()
    {
        _spellBar.Init();
        _inventoryView.Instantiate();
    }

    private void Bind()
    {
        UserInput input = Container.Instantiate<UserInput>();
        input.Enable();
        Container.Bind<UserInput>().FromInstance(input).AsSingle().NonLazy();
        Container.Bind<PoolService>().FromInstance(_poolService).AsSingle().NonLazy();
        Container.Bind<GameStateBuilder>().FromNew().AsSingle().NonLazy();
        Container.Bind<ActiveEnemies>().FromInstance(new ActiveEnemies(_enemyFactory)).AsSingle().NonLazy();

        Container.Bind<LootBoxUnpacker>().FromInstance(_boxUnpacker).AsSingle().NonLazy();
        Container.Bind<SpellInventory>().FromNew().AsSingle().NonLazy();
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
        Container.Inject(_inventoryView);

        foreach (BaseSpellEffect effect in _spellEffects)
            Container.Inject(effect);

        foreach (BaseAdditionalEnemySelector additionalSelector in _additionalSelectors)
            Container.Inject(additionalSelector);
    }
}