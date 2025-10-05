using System;
using System.Collections;
using System.Linq;
using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using UnityEngine;

namespace Banchy
{
    public class UnitSpawnSystem
    {
        public UnitSpawnerPrefs Prefs { get; private set; }

        public float CurSpawnTime { get; set; } = 0f;
        
        public void Init()
        {
            RefreshPrefs();
            StartLifecycle();
        }

        public void StartLifecycle()
        {
            Vars.Instance.sceneInjection.StartCoroutine(LifecycleCoroutine());
        }
        public IEnumerator LifecycleCoroutine()
        {
            while (true)
            {
                while (CurSpawnTime < Prefs.SpawnDelay || Vars.Instance.systems.UnitSystem.All.Count >= Prefs.MaxUnits)
                {
                    CurSpawnTime += Time.deltaTime;
                    yield return null;
                }

                while (!Vars.Instance.state.IsGame())
                {
                    yield return null;
                }
                Spawn();
                ResetSpawnTime();
            }
        }

        public void Spawn()
        {
            foreach (var pref in Prefs.Prefs)
            {
                if (pref == Prefs.Prefs.Last() || pref.ShouldSpawn())
                {
                    pref.UnitType.ToUnit().Init();
                    break;
                }
            }
        }
        private void ResetSpawnTime()
        {
            CurSpawnTime = 0;
        }

        public void RefreshPrefs()
        {
            Prefs = AllUnitSpawnerPrefs.Bonfire1;
            int bonfiresCount = Vars.Instance.systems.BuildingSystem.Bonfires.Count;
            foreach (var prefs in AllUnitSpawnerPrefs.All)
            {
                if (bonfiresCount < prefs.RequiredBonfires || bonfiresCount <= Prefs.RequiredBonfires)
                {
                    continue;
                }

                Prefs = prefs;
            }
        }
    }
}