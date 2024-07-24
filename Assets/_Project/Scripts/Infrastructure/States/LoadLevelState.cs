namespace OctanGames.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        void IExitableState.Exit()
        {
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