using System;
using Blaze;
using UnityEngine;
using UnityEngine.Serialization;

namespace Banchy
{
    public class _World : MonoBehaviour
    {
        public ItemsBootstrap itemsBootstrap;
        public BuildingsBootstrap buildingsBootstrap;
        public WeaponsBootstrap weaponsBootstrap;
        public UnitsBootstrap unitsBootstrap;
        
        public CRect worldRect;
        
        public void Init()
        {
            itemsBootstrap.Init();
            buildingsBootstrap.Init();
            weaponsBootstrap.Init();
            unitsBootstrap.Init();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(worldRect.Position, worldRect.Size);
        }
    }
}