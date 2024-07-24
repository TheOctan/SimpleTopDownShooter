using OctanGames.Infrastructure.States;
using OctanGames.Services;

namespace OctanGames.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
    
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container);
        }
    }
}