using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Banchy
{
    public class UI : MonoBehaviour
    {
        public BonfireTimeFragment  bonfireTimeFragment;
        
        public RectTransform crosshairTransform;
        public GameObject crosshairRoot;

        public GameObject loseScreenRoot;
        public Button resetButton;

        public TMP_Text bonfiresCountText;
        
        public DialogScreen dialogScreen;

        public Image hpBar;

        public TMP_Text bonesText;
        
        public void Init()
        {
            loseScreenRoot.SetActive(false);
            resetButton.onClick.AddListener(() => Vars.Instance.resetter.Reset());
            
            dialogScreen.Init();
        }

        public void _Update()
        {
            bonfireTimeFragment._Update();
            bonfiresCountText.text = $"Bonfires burning: {Vars.Instance.systems.BuildingSystem.Bonfires.Count}";
            
            dialogScreen._Update();
            
            Player player = Vars.Instance.player;
            hpBar.fillAmount = player.Health / player.Type.MaxHealth;

            bonesText.text = $"You have {Vars.Instance.player.inventory.Items.Count} / {Vars.Instance.systems.BonfireBuildingSystem.CurBonfire.Recipe.Cost.First().Amount} bones for bonfire";
        }
    }
}
