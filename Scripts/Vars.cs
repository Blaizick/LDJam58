using Unity.VisualScripting;
using UnityEngine;

namespace Banchy
{
    public class Vars : MonoBehaviour
    {
        public static Vars Instance { get; private set; }

        public UI ui;
        
        public Player player;
        public DesktopInput input;

        public Content content;
        
        public Systems systems;
        
        public _World world;
        
        private void Awake()
        {
            Instance = this;
            
            input = new DesktopInput();
            systems = new Systems();
            
            content.Init();
            systems.Init();
            world.Init();
            player.Init();
            input.Init();
            ui.Init();
        }

        private void Update()
        {
            input._Update();
            player._Update();
            systems._Update();
            ui._Update();
        }
    }
}
