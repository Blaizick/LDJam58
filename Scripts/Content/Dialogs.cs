using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public static class Dialogs
    {
        public static Dialog Start { get; set; }
        public static Dialog Bonfire2 { get; set; }
        public static Dialog Bonfire3 { get; set; }
        public static Dialog Bonfire4 { get; set; }
        
        public static List<Dialog> All { get; set; }
        
        public static void Init()
        {
            Start = new Dialog()
            {
                ShouldShow = () => true,
                Knot = "Start"
            };
            Bonfire2 = new Dialog()
            {
                ShouldShow = () =>
                {
                    // Debug.Log(Vars.Instance.systems.BuildingSystem.Bonfires.Count);
                    return Vars.Instance.state.IsGame() && Vars.Instance.systems.BuildingSystem.Bonfires.Count >= 2;
                },
                Knot = "Bonfire2"
            };
            Bonfire3 = new Dialog()
            {
                ShouldShow = () =>
                {
                    return Vars.Instance.state.IsGame() && Vars.Instance.systems.BuildingSystem.Bonfires.Count >= 3;
                },
                Knot = "Bonfire3"
            };
            Bonfire4 = new Dialog()
            {
                ShouldShow = () =>
                {
                    return Vars.Instance.state.IsGame() && Vars.Instance.systems.BuildingSystem.Bonfires.Count >= 4;
                },
                Knot = "Bonfire4"
            };
            
            All = new List<Dialog>()
            {
                Start,
                Bonfire2,
                Bonfire3,
                Bonfire4
            };
        }
    }
    public class Dialog
    {
        public Func<bool> ShouldShow { get; set; }
        public string Knot { get; set; }
    }
}