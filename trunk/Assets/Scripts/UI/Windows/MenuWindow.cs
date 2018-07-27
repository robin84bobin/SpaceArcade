using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows
{
    public class MenuWindow : BaseWindow 
    {
        public Text loadingStatusText;
        public Button startButton;
        public Button quitButton;

        public Text hintText;

        public static void Show()
        {
            Main.Instance.UI.ShowWindow("MenuWindow");
            
        }

        public override void OnShowComplete(WindowParams param_ = null)
        {
            base.OnShowComplete (param_);
            startButton.onClick.AddListener (OnStartButton);
            quitButton.onClick.AddListener(OnQuitButton);

            hintText.enabled = SystemInfo.deviceType == DeviceType.Desktop;
        }

        void OnLoadProgressEvent (string message_)
        {
            loadingStatusText.text = message_;
        }

        protected override void OnHide ()
        {
            base.OnHide ();

        }

        public void OnStartButton ()
        {
            Hide ();
            Main.Instance.LoadScene("level");
        }

        private void OnQuitButton()
        {
            Application.Quit();
        }
    }
}
