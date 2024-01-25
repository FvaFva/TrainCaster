using UnityEngine;
using Zenject;

public class MainSceneInjection : MonoInstaller
{
    [SerializeField] private Caster _caster;
    [SerializeField] private Train _train;
    [SerializeField] private SpellBar _spellBar;
    [SerializeField] private PoolService _poolService;

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
        Container.Bind<GameStates>().FromNew().AsSingle().NonLazy();

    }

    private void Inject()
    {
        Container.InjectGameObject(_caster.gameObject);
        Container.InjectGameObject(_train.gameObject);
    }
}