using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Banchy
{
    public class DialogScreen : MonoBehaviour
    {
        public GameObject dialogScreenRoot;
        public Button dialogButton;
        public TMP_Text dialogText;
        public TMP_Text speecherText;

        private Coroutine TypingCoroutine { get; set; }
        
        public static readonly float AnimatedCharsPerSec = 20f; 
        
        public void Init()
        {
            dialogButton.onClick.AddListener(() =>
            {
                DialogManager dialogManager = Vars.Instance.dialogManager;
                if (dialogManager.Running)
                {
                    if (TypingText())
                    {
                        StopTypingText();                        
                    }
                    else
                    {
                        dialogManager.TryContinue();
                    }
                }
            });
        }

        public void _Update()
        {
            DialogManager dialogManager = Vars.Instance.dialogManager;
            
            dialogScreenRoot.SetActive(dialogManager.Running);
            
            if (dialogManager.Running)
            {
                if (dialogText.text != dialogManager.Text && !TypingText())
                {
                    speecherText.text = dialogManager.SpeechersName;
                    StartTypingText(dialogManager.Text);
                }
            }
        }


        public void StartTypingText(string text)
        {
            TypingCoroutine = StartCoroutine(TypeTextCoroutine());
        }
        public void StopTypingText()
        {
            if (TypingCoroutine != null)
            {
                StopCoroutine(TypingCoroutine);
                TypingCoroutine = null;
            }
            dialogText.text = Vars.Instance.dialogManager.Text;
        }

        public bool TypingText()
        {
            return TypingCoroutine != null;
        }
        private IEnumerator TypeTextCoroutine()
        {
            string text = Vars.Instance.dialogManager.Text;
            dialogText.text = "";
            float t = 0;
            int charIndex = 0;

            while (charIndex < text.Length)
            {
                t += Time.unscaledDeltaTime * AnimatedCharsPerSec;
                charIndex = Mathf.Min(text.Length, Mathf.FloorToInt(t));
                dialogText.text = text.Substring(0, charIndex);
                yield return null;
            }
            
            StopTypingText();
        }
    }
}