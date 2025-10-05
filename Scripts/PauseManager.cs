using UnityEngine;

namespace Banchy
{
    public class PauseManager
    {
        public void Pause()
        {
            Time.timeScale = 0f;
            Vars.Instance.state.State = GameState.Paused;
        }
        public void Resume()
        {
            Time.timeScale = 1f;
            Vars.Instance.state.State = GameState.Running;
        }
    }
}