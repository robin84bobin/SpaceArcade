using System;
using Assets.Scripts.UI.Windows.InfoWindows;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.UI.Windows
{
    public class GameOverWindow : BaseWindow 
    {
        public Text stateHeaderText;
        public Text scoreValueText;

        public Button quitButton;
        public Button restartButton;
        public Button menuButton;

        public static void Show()
        {
            Main.Instance.UI.ShowWindow("GameOverWindow");
        }

        public override void OnShowComplete(WindowParams param_ = null)
        {
            base.OnShowComplete(param_);

            quitButton.onClick.AddListener(OnQuitButton);
            restartButton.onClick.AddListener(OnRestartButton);
            menuButton.onClick.AddListener(OnMenuButton);
        }

        private void OnMenuButton()
        {
            MenuWindow.Show();
        }

        private void OnRestartButton()
        {
            Main.Instance.LoadScene("level");
        }

        private void OnQuitButton()
        {
            Application.Quit();
        }
    }
}
