namespace Banchy
{
    public class GameStateContainer
    {
        public GameState State { get; set; }

        public void Init()
        {
            State = GameState.Running;
        }
        
        public bool IsGame()
        {
            return State == GameState.Running;
        }
        public bool IsLose()
        {
            return State == GameState.Lose;
        }
        public bool IsWin()
        {
            return State == GameState.Win;
        }
        public bool IsPaused()
        {
            return State == GameState.Paused;
        }
    }

    public enum GameState
    {
        Running,
        Lose,
        Win,
        Paused
    }
}