using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class WeaponsBootstrap : MonoBehaviour
    {
        public List<WeaponBootstrap> bootstraps = new();

        public void Init()
        {
            foreach (WeaponBootstrap bootstrap in bootstraps)
            {
                bootstrap.Init();
            }
        }
    }
}