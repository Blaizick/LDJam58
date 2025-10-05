using Ink.Runtime;
using UnityEngine;

namespace Banchy
{
    public class DialogManager : MonoBehaviour
    {
        public TextAsset dialogAsset;
        private Story story;

        public bool Running { get; set; } = false;
        public string SpeechersName { get; set; } = null;
        public string Text { get; set; } = null;
        public Dialog CurDialog { get; private set; }

        public void Init()
        {
            story = new Story(dialogAsset.text);
        }

        public void Reset()
        {
            foreach (var dialog in Dialogs.All)
            {
                SetDialogListened(dialog.Knot, false);
            }
        }
        
        public void _Update()
        {
            if (Running)
            {
                return;
            }
            foreach (var dialog in Dialogs.All)
            {
                if (GetDialogListened(dialog.Knot))
                {
                    continue;
                }

                if (dialog.ShouldShow())
                {
                    CurDialog = dialog;
                    StartDialog(dialog);
                    break;
                }
            }  
        }
        
        public void StartDialog(Dialog dialog)
        {
            // Debug.Log(dialog.Knot);
            CurDialog = dialog;
            Running = true;
            story.ChoosePathString(dialog.Knot);
            TryContinue();
            Vars.Instance.pauseManager.Pause();
        }
        public bool TryContinue()
        {
            if (story.canContinue)
            {
                Text = story.Continue();
                SpeechersName = story.currentTags.Count > 0 ? story.currentTags[0] : "";
                return true;
            }
            EndDialog();
            return false;
        }

        public void EndDialog()
        {
            Text = null;
            Running = false;
            SetDialogListened(CurDialog.Knot, true);
            Vars.Instance.pauseManager.Resume();
        }

        public bool GetDialogListened(string knot)
        {
            return PlayerPrefs.GetInt($"Dialog: {knot}", 0) == 1;
        }
        public void SetDialogListened(string knot, bool listened)
        {
            PlayerPrefs.SetInt($"Dialog: {knot}", listened ? 1 : 0);
        }
    }
}