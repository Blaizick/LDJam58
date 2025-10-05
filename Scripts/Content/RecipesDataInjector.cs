using System.Collections.Generic;

namespace Banchy
{
    public class Recipe
    {
        public ItemStack[] Cost { get; set; }
    }
    
    public static class Recipes
    {
        public static Recipe Bonfire2 { get; set; }
        public static Recipe Bonfire3 { get; set; }
        public static Recipe Bonfire4 { get; set; }
        
        public static List<Recipe> All { get; set; }
        
        public static void Init()
        {
            Bonfire2 = new Recipe()
            {
                Cost = new ItemStack[]
                {
                    new ItemStack(Items.SmallBone, 5)
                }
            };
            Bonfire3 = new Recipe()
            {
                Cost = new ItemStack[]
                {
                    new ItemStack(Items.SmallBone, 10)
                }
            };
            Bonfire4 = new Recipe()
            {
                Cost = new ItemStack[]
                {
                    new ItemStack(Items.SmallBone, 16)
                }
            };

            All = new List<Recipe>()
            {
                Bonfire2, Bonfire3, Bonfire4
            };
        }
    }
}