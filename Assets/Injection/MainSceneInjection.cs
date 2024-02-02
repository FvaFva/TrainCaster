using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInjection : MonoInstaller
{
    [SerializeField] private Caster _caster;
    [SerializeField] private Train _train;
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
    }

    private void Bind()
    {
        UserInput instance = Container.Instantiate<UserInput>();
        instance.Enable();
        Container.Bind<UserInput>().FromInstance(instance).AsSingle().NonLazy();
        Container.Bind<PoolService>().FromInstance(_poolService).AsSingle().NonLazy();
        Container.Bind<GameState>().FromNew().AsSingle().NonLazy();
        Container.Bind<ActiveEnemies>().FromInstance(new ActiveEnemies(_enemyFactory)).AsSingle().NonLazy();
    }

    private void Inject()
    {
        Container.InjectGameObject(_caster.gameObject);
        Container.InjectGameObject(_train.gameObject);
        Container.InjectGameObject(_enemyFactory.gameObject);
        Container.InjectGameObject(_gameStateMachine.gameObject);
        Container.InjectGameObject(_base.gameObject);

        foreach (BaseSpellEffect effect in _spellEffects)
            Container.Inject(effect);

        foreach (BaseAdditionalEnemySelector additionalSelector in _additionalSelectors)
            Container.Inject(additionalSelector);
    }
}