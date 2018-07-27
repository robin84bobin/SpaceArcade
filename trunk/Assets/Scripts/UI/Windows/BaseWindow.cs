using UnityEngine;

namespace Assets.Scripts.UI.Windows
{
    public class BaseWindow : MonoBehaviour 
    {
        protected WindowParams windowsParameters;

        public void Hide ()
        {
            OnHide();
            Main.Instance.UI.HideWindow(this);
        }

        public virtual void OnShowComplete(WindowParams param = null)
        {
            windowsParameters = param;
        }

        protected virtual void OnHide()
        {
        }
    }
}
