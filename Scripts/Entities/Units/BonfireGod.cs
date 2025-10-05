using System.Collections;

namespace Banchy
{
    public class BonfireGodType : UnitType
    {
        public Weapon RoundShootWeapon { get; set; }
    }
    public class BonfireGod : Unit
    {
        public BonfireGodType BonfireGodType
        {
            get
            {
                return (BonfireGodType)Type;
            }
        }

        public override void Init()
        {
            base.Init();
            
            StartLifecycle();
        }
        public void StartLifecycle()
        {
            Vars.Instance.sceneInjection.StartCoroutine(LifecycleCoroutine());
        }
        public IEnumerator LifecycleCoroutine()
        {
            yield return null;
        }
    }
}