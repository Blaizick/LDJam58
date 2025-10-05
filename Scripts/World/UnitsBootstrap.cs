using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class UnitsBootstrap : MonoBehaviour
    {
        public List<UnitBootstrap> bootsraps = new();

        public void Init()
        {
            bootsraps.ForEach(b => b.Init());
        }
    }
}