using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Banchy
{
    [System.Serializable]
    public class BonfireData : BuildingData
    {
        public Sprite deadSprite;
        
        public override void Inject(BuildingType buildingType)
        {
            base.Inject(buildingType);

            ((Bonfire)buildingType).DeadSprite = deadSprite;
        }
    }
    
    public class Bonfire : BuildingType
    {
        /// <summary>
        /// In seconds 
        /// </summary>
        public float StartRemainingTime { get; set; }
        /// <summary>
        /// In seconds 
        /// </summary>
        public float MaxRemainingTime { get; set; }
        
        public Sprite DeadSprite { get; set; }
    }
    
    public class BonfireBuilding : Building
    {
        /// <summary>
        /// In seconds 
        /// </summary>
        public float RemainingTime { get; set; }

        public TMP_Text timeRemainingText;
        
        public AudioSource audioSource;
        
        public Bonfire Bonfire
        {
            get
            {
                return (Bonfire)Type;
            }
        }
        
        public override void Init()
        {
            base.Init();
            
            RemainingTime = Bonfire.StartRemainingTime;

            Systems systems = Vars.Instance.systems;
            
            systems.UnitSpawnSystem.RefreshPrefs();
            systems.BonfireBuildingSystem.Refresh();
        }

        public override void _Update()
        {
            if (!Dead)
            {
                if (RemainingTime > 0)
                {
                    RemainingTime -= Time.deltaTime;
                }
                else
                {
                    _Destroy();
                }
            }
            
            UpdateText();
            
            base._Update();
        }


        public override void _Destroy()
        {
            Unregister();
            Dead = true;
            RefreshSprite();
            UpdateText();
            audioSource.mute = true;
            Vars.Instance.systems.LoseSystem.RefreshLose();
        }

        public override void RefreshSprite()
        {
            spriteRenderer.sprite = Dead ? Bonfire.DeadSprite : Bonfire.Sprite;
        }

        public float GetAcceptedRemainingTime(float time)
        {
            float max = Bonfire.MaxRemainingTime;
            float pred = time + RemainingTime;
            if (pred > max)
            {
                return time - (pred - max);
            }
            return time;
        }
        public void AddRemainingTime(float time)
        {
            RemainingTime += GetAcceptedRemainingTime(time);
        }

        private void UpdateText()
        {
            timeRemainingText.text = Dead ? "Dead" : $"Time remaining: {RemainingTime}";
        }
        
    }
}