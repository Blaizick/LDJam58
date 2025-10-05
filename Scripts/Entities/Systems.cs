using System.Collections.Generic;

namespace Banchy
{
    public class Systems
    {
        public ItemSystem ItemSystem { get; private set; } = new();
        public BuildingSystem BuildingSystem { get; private set; } = new();
        public WeaponSystem WeaponSystem { get; private set; } = new();
        public AmmoSystem AmmoSystem { get; private set; } = new();
        public UnitSystem UnitSystem { get; private set; } = new();
        public UnitSpawnSystem UnitSpawnSystem { get; private set; } = new();
        public AffectsSystem AffectSystem { get; private set; } = new();
        public BonfireBuildingSystem BonfireBuildingSystem { get; private set; } = new();
        public LoseSystem LoseSystem { get; private set; } = new();
        
        public void Init()
        {
            UnitSpawnSystem.Init();
            BonfireBuildingSystem.Init();
        }

        public void _Update()
        {
            ItemSystem._Update();
            BuildingSystem._Update();
            WeaponSystem._Update();
            AmmoSystem._Update();
            UnitSystem._Update();
            AffectSystem._Update();
            LoseSystem._Update();
        }
    }

    public class CSystem<T> where T : IUpdate
    {
        public virtual List<T> All { get; set; } = new();
        public virtual List<T> DelayedRemoves { get; private set; } = new();
        
        public virtual void Add(T item)
        {
            All.Add(item);
        }
        public virtual void RemoveDelayed(T item)
        {
            DelayedRemoves.Add(item);
        }

        public virtual void Remove(T item)
        {
            All.Remove(item);
        }

        public virtual void _Update()
        {
            foreach (var bullet in All)
            {
                bullet._Update();
            }

            foreach (var delayedRemove in DelayedRemoves)
            {
                Remove(delayedRemove);
            }
            DelayedRemoves.Clear();
        }
    }

    public interface IUpdate
    {
        public void _Update();
    }
}