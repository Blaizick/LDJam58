using UnityEngine;

namespace Banchy
{
    public class LoseSystem : IUpdate
    {
        public void _Update()
        {
            RefreshLose();
        }

        public void RefreshLose()
        {
            if (Vars.Instance.player == null || Vars.Instance.player.Dead || Vars.Instance.systems.BuildingSystem.Bonfires.Count <= 0)
            {
                Lose();
            }
        }
        
        public void Lose()
        {
            Vars.Instance.ui.loseScreenRoot.SetActive(true);
            Vars.Instance.state.State = GameState.Lose;
            Time.timeScale = 0f;
        }
    }
}