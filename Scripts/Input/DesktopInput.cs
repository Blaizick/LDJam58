using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Banchy
{
    public class DesktopInput : MonoBehaviour
    {
        public InputSystem_Actions Actions { get; set; }

        public Vector2 CursorPosition { get; private set; } = Vector2.zero;
        public Vector2 CursorWorldPosition { get; private set; } = Vector2.zero;
        
        public SPlan sPlan;

        public void Init()
        {
            Actions = new InputSystem_Actions();
            Actions.Enable();
            
            sPlan.Init();
            sPlan.Hide();
        }

        public void _Update()
        {
            if (!Vars.Instance.state.IsGame())
            {
                return;
            }
            
            Player player = Vars.Instance.player;
            
            CursorPosition = Actions.Player.CursorPosition.ReadValue<Vector2>();
            CursorWorldPosition = Camera.main.ScreenToWorldPoint(CursorPosition);
            
            player.InputMovement = Actions.Player.Movement.ReadValue<Vector2>();

            if (Actions.Player.Take.IsPressed())
            {
                Take();
            }
            if (Actions.Player.Throw.WasPressedThisFrame())
            {
                TryThrow();
            }

            
            sPlan._gameobject.transform.position = CursorWorldPosition;
            
            if (Actions.Player.SelectBonfire.IsPressed())
            {
                SelectBonfire();
            }
            if (Actions.Player.Deselect.IsPressed())
            {
                DeselectBuildingType();
            }

            sPlan._Update();
            
            player.weapons.CursorWorldPos = CursorWorldPosition;
            
            if (Actions.Player.Place.IsPressed() && !EventSystem.current.IsPointerOverGameObject())
            {
                if (sPlan.Selected)
                {
                    if (TryPlace())
                    {
                        DeselectBuildingType();
                    }
                }
                else
                {
                    player.weapons.TryShoot();
                }
            }
            
            Vars.Instance.ui.crosshairTransform.anchoredPosition = CursorPosition;
            Vars.Instance.ui.crosshairRoot.SetActive(!sPlan.Selected);
        }

        public void OnDestroy()
        {
            Actions.Disable();
        }
        
        public void Take()
        {
            Player player = Vars.Instance.player;
            
            List<MonoItem> items = Vars.Instance.systems.ItemSystem.GetItemsWithin(player._gameObject.transform.position, player.TakeRange);

            foreach (MonoItem item in items)
            {
                if (Vars.Instance.player.inventory.CanTake(item.Type))
                {
                    Vars.Instance.player.inventory.Take(item);
                    break;
                }
            }
        }

        public bool TryThrow()
        {
            Player player = Vars.Instance.player;
            
            foreach (InventoryItem item in player.inventory.Items)
            {
                if (!(item.Type is BurningItemType))
                {
                    continue;
                }

                float burningTime = ((BurningItemType)item.Type).BurningTime;
                
                List<BonfireBuilding> bonfires = Vars.Instance.systems.BuildingSystem.Bonfires;
                if (bonfires.Count <= 0)
                {
                    break;
                }

                BonfireBuilding minRemainingBf = null;
                float minRemaining = int.MaxValue;
                foreach (BonfireBuilding b in bonfires)
                {
                    if (Vector2.Distance(b._gameObject.transform.position, player._gameObject.transform.position) >
                        player.ThrowDistance)
                    {
                        continue;
                    }
                    
                    float remaining = b.RemainingTime;
                    if (remaining < minRemaining)
                    {
                        minRemaining = remaining;
                        minRemainingBf = b;
                    }
                }

                if (minRemainingBf != null)
                {
                    Throw(minRemainingBf, item);

                    return true;
                }

                break;
            }

            return false;
        }

        public void Throw(BonfireBuilding build, InventoryItem item)
        {
            Player player = Vars.Instance.player;

            ThrownItem thrownItem = item.Type.ToThrownItem();
            thrownItem._gameObject.transform.position = item._gameObject.transform.position;
            thrownItem.Init();
            thrownItem.Throw(build._gameObject.transform.position);
            
            player.inventory.Remove(item);
            item._Destroy();
            
            build.AddRemainingTime(((BurningItemType)item.Type).BurningTime);
        }

        public bool TryPlace()
        {
            if (sPlan.Type.CanPlace(sPlan._gameobject.transform.position))
            {
                Vars.Instance.player.inventory.TakeAll(sPlan.Type.Recipe.Cost);
                Building build = sPlan.Type.ToBuilding();
                build._gameObject.transform.position = sPlan._gameobject.transform.position;
                build.Init();
                
                return true;
            }
            
            return false;
        }


        public void SelectBonfire()
        {
            SelectBuildingType(Vars.Instance.systems.BonfireBuildingSystem.CurBonfire);
        }
        public void SelectBuildingType(BuildingType buildType)
        {
            sPlan.Show(buildType);
        }
        public void DeselectBuildingType()
        {
            sPlan.Hide();
        }
    }
}