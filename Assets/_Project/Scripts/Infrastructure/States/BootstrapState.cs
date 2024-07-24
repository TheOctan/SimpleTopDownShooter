using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Services;

namespace OctanGames.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Initial";
        private const string GAMEPLAY_SCENE = "Gameplay";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = services;

            RegisterServices();
        }

        void IState.Enter()
        {
            _sceneLoader.Load(INITIAL_SCENE, onLoaded: EnterLoadLevel);
        }

        void IExitableState.Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GAMEPLAY_SCENE);
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IGameStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
        }
    }
}