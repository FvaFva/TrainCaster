using UnityEngine;
using Zenject;

public class MainSceneInjection : MonoInstaller
{
    [SerializeField] private Caster _caster;
    [SerializeField] private Train _train;
    [SerializeField] private SpellBar _spellBar;

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
    }

    private void Inject()
    {
        Container.InjectGameObject(_caster.gameObject);
        Container.InjectGameObject(_train.gameObject);
    }
}