namespace Banchy
{
    public class Systems
    {
        public ItemSystem itemSystem = new();
        public BuildingSystem buildingSystem = new();
        
        public void Init()
        {
            
        }

        public void _Update()
        {
            itemSystem._Update();
            buildingSystem._Update();
        }
    }
}