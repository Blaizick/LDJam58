using Blaze;
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

        public Effects effects;
        
        public SceneInjection sceneInjection;
        public CGizmos cGizmos;
        
        public Resetter resetter;
        public GameStateContainer state;
        
        public CameraManager cameraManager;
        public DialogManager dialogManager;
        public PauseManager pauseManager;
        
        private void Awake()
        {
            Instance = this;
            
            Time.timeScale = 1f;
            
            state = new GameStateContainer();
            systems = new Systems();
            pauseManager = new PauseManager();
            
            state.Init();
            content.Init();
            systems.Init();
            world.Init();
            effects.Init();
            input.Init();
            ui.Init();
            dialogManager.Init();
        }

        private void Update()
        {
            input._Update();
            systems._Update();
            ui._Update();
            cameraManager._Update();
            dialogManager._Update();
        }
    }
}
