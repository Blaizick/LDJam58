using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class UnitSpawnerPrefs
    {
        public List<UnitSpawnerPref> Prefs { get; set; } = new();
        
        public float RequiredBonfires { get; set; }
        public float SpawnDelay { get; set; }
        public int MaxUnits { get; set; }
        public UnitType Boss { get; set; }
    }

    public class UnitSpawnerPref
    {
        public float Chance { get; set; }
        public UnitType UnitType { get; set; }

        public bool ShouldSpawn()
        {
            return Random.Range(0f, 1f) <= Chance;
        }
    }

    public static class AllUnitSpawnerPrefs
    {
        public static UnitSpawnerPrefs Bonfire1 { get; set; }
        public static UnitSpawnerPrefs Bonfire2 { get; set; }
        public static UnitSpawnerPrefs Bonfire3 { get; set; }
        public static UnitSpawnerPrefs Bonfire4 { get; set; }

        public static List<UnitSpawnerPrefs> All { get; set; }
        
        public static void Init()
        {
            Bonfire1 = new UnitSpawnerPrefs()
            {
                Prefs = new List<UnitSpawnerPref>()
                {
                    new UnitSpawnerPref()
                    {
                        Chance = 1f,
                        UnitType = Units.Seal,
                    }
                },
                SpawnDelay = 8f,
                MaxUnits = 3,
                RequiredBonfires = 1
            };
            Bonfire2 = new UnitSpawnerPrefs()
            {
                Prefs = new List<UnitSpawnerPref>()
                {
                    new UnitSpawnerPref()
                    {
                        Chance = 0.15f,
                        UnitType = Units.Seal
                    },
                    new UnitSpawnerPref()
                    {
                        Chance = 0.85f,
                        UnitType = Units.Cubidum,
                    }
                },
                SpawnDelay = 7f,
                MaxUnits = 4,
                RequiredBonfires = 2
            };
            Bonfire3 = new UnitSpawnerPrefs()
            {
                Prefs = new List<UnitSpawnerPref>()
                {
                    new UnitSpawnerPref()
                    {
                        Chance = 0.03f,
                        UnitType = Units.Seal
                    },
                    new UnitSpawnerPref()
                    {
                        Chance = 0.17f,
                        UnitType = Units.Cubidum
                    },
                    new UnitSpawnerPref()
                    {
                        Chance = 0.8f,
                        UnitType = Units.Egy,
                    }
                },
                SpawnDelay = 6f,
                MaxUnits = 5,
                RequiredBonfires = 3
            };
            Bonfire4 = new UnitSpawnerPrefs()
            {
                Prefs = new List<UnitSpawnerPref>()
                {
                    new UnitSpawnerPref()
                    {
                        Chance = 0.2f,
                        UnitType = Units.Cubidum
                    },
                    new UnitSpawnerPref()
                    {
                        Chance = 0.8f,
                        UnitType = Units.Egy,
                    }
                },
                SpawnDelay = 3f,
                MaxUnits = 8,
                Boss = Units.BonfireGod,
                RequiredBonfires = 4
            };

            All = new List<UnitSpawnerPrefs>(new UnitSpawnerPrefs[]
            {
                Bonfire1,
                Bonfire2,
                Bonfire3,
                Bonfire4
            });
        }
    }
}