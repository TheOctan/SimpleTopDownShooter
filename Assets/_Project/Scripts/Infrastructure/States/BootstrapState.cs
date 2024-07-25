using System;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Factory;
using OctanGames.Services;
using OctanGames.Services.Input;
using OctanGames.StaticData;
using UnityEngine;

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
            _serviceLocator.RegisterSingle(InputService());
            _serviceLocator.RegisterSingle<IGameStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());

            RegisterStaticData();

            var assetProvider = _serviceLocator.Single<IAssetProvider>();
            var inputService = _serviceLocator.Single<IInputService>();
            var staticDataService = _serviceLocator.Single<IStaticDataService>();
            _serviceLocator.RegisterSingle<IGameFactory>(
                new GameFactory(assetProvider, inputService, staticDataService));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadAllStaticData();

            _serviceLocator.RegisterSingle(staticData);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor) return new StandaloneInputService();
            if (Application.isMobilePlatform) return new MobileInputService();
            throw new NotSupportedException("Input is not supported on this platform");
        }
    }
}