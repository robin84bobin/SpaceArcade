using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Windows
{
    /// <summary>
    /// Preloader window for scene loading process
    /// </summary>
    public class Preloader : BaseWindow
    {
        public Text progressText;

        private static string _sceneName;

        /// <summary>
        /// Shows preloader
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static Preloader Show(string sceneName)
        {
            _sceneName = sceneName;
            return Main.Instance.UI.ShowWindow("PreloaderWindow") as Preloader;
        }

        private AsyncOperation _async;

        /// <summary>
        /// Calls on window instantiation complete
        /// </summary>
        /// <param name="param"></param>
        public override void OnShowComplete(WindowParams param = null)
        {
            base.OnShowComplete(param);
            Init(_sceneName);
        }

        /// <summary>
        /// Preloader initialization and start concrete scene loading 
        /// </summary>
        /// <param name="levelName"></param>
        internal void Init(string levelName)
        {
            StartCoroutine(LoadLevel(levelName));
        }

        /// <summary>
        /// Async level loading
        /// </summary>
        /// <param name="levelName"></param>
        /// <returns></returns>
        private IEnumerator LoadLevel(string levelName)
        {
            _async = SceneManager.LoadSceneAsync(levelName);
            yield return _async;
        }

        /// <summary>
        /// Check if loading in progress and
        /// hide preloader if loading complete
        /// </summary>
        void Update()
        {
            
            if (_async != null)
            {
                Debug.Log("Loading level:" + _async.progress.ToString());
                setProgress(_async.progress);
                if (_async.progress == 1)
                {
                    _async = null;
                    Hide();
                }
            }
        }

        /// <summary>
        /// Display loading progress
        /// </summary>
        /// <param name="progress"></param>
        void setProgress(float progress)
        {
            progressText.text = (progress * 100f).ToString() + "%";
        }
    }
}
