using System.Collections.Generic;

namespace Banchy
{
    public class AffectsSystem
    {
        public float ExtDamage { get; private set; }
        public float ReloadReduce { get; private set; }
        public int ExtInventorySlots { get; private set; }
        public float ExtPlayerSpeed { get; private set; }
        
        public void _Update()
        {
            Refresh();
        }

        public void Refresh()
        {
            ExtDamage = 0;
            ReloadReduce = 0;
            ExtInventorySlots = 0;
            ExtPlayerSpeed = 0;
            
            foreach (var affect in Banchy.Affects.All)
            {
                if (affect.ShouldEnable())
                {
                    ExtDamage += affect.ExtDamage;
                    ReloadReduce += affect.ReloadSpeedReduce;
                    ExtInventorySlots += affect.ExtInventorySlots;
                    ExtPlayerSpeed += affect.ExtPlayerSpeed;
                }
            }
        }
    }
}