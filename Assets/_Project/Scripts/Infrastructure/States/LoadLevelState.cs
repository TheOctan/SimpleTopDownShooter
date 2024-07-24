using OctanGames.Infrastructure.Factory;
using OctanGames.Logic;
using UnityEngine;

namespace OctanGames.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        void IExitableState.Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitialGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitialGameWorld()
        {
            GameObject player = InitPlayer();
        }

        private GameObject InitPlayer()
        {
            return _gameFactory.CreateHero(Vector3.zero);
        }
    }
}