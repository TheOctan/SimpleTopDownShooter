using OctanGames.Logic;

namespace OctanGames.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
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
        }

    }
}