using UnityEngine;

namespace Banchy
{
    public class Content : MonoBehaviour
    {
        public ItemsDataInjector itemsInjector;
        public BuildingsDataInjector buildingsInjector;
        public UnitsDataInjector unitsInjector;
        public AmmoDataInjector ammoInjector;
        public WeaponsDataInjector weaponsInjector;
        
        public void Init()
        {
            Items.Init();
            Recipes.Init();
            Buildings.Init();
            Ammos.Init();
            Weapons.Init();
            Units.Init();
            AllUnitSpawnerPrefs.Init();
            Affects.Init();
            Dialogs.Init();
            
            itemsInjector.Inject();
            buildingsInjector.Inject();
            ammoInjector.Inject();
            weaponsInjector.Inject();
            unitsInjector.Inject();
        }
    }
}