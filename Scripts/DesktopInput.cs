using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

namespace Banchy
{
    public class DesktopInput
    {
        public InputSystem_Actions actions;
        
        public void Init()
        {
            actions = new InputSystem_Actions();
            actions.Enable();
        }

        public void _Update()
        {
            Player player = Vars.Instance.player;
            
            player.InputMovement = actions.Player.Movement.ReadValue<Vector2>();

            if (actions.Player.Take.IsPressed())
            {
                Take();
            }
            if (actions.Player.Throw.IsPressed())
            { 
                Throw();
            }
        }
        
        public void Take()
        {
            Player player = Vars.Instance.player;
            
            List<MonoItem> items = Vars.Instance.systems.itemSystem.GetItemsWithin(player._gameObject.transform.position, player.TakeRange);

            foreach (MonoItem item in items)
            {
                if (Vars.Instance.player.inventory.CanTake(item))
                {
                    Vars.Instance.player.inventory.Take(item);
                    break;
                }
            }
        }

        public void Throw()
        {
            Player player = Vars.Instance.player;
            
            foreach (MonoItem item in player.inventory.Items)
            {
                if (!(item.Type is BurningItemType))
                {
                    continue;
                }
                // Debug.Log("item.Type is BurningItemType");
                List<MonoBuilding> bonfires = Vars.Instance.systems.buildingSystem.Buildings.FindAll(b => b is BonfireBuilding);
                if (bonfires.Count <= 0)
                {
                    continue;
                }
                foreach (MonoBuilding b in bonfires)
                {
                    if (Vector2.Distance(b._gameObject.transform.position, player._gameObject.transform.position) >
                        player.ThrowDistance)
                    {
                        continue;
                    }
                    
                    ((BonfireBuilding)b).RemainingTime +=
                        ((BurningItemType)item.Type).BurningTime;
                                    
                    player.inventory.Items.Remove(item);
                    item._Destroy();
                }
                // Debug.Log("Bonfires.Count > 0");
                break;
            }
        }


    }
}