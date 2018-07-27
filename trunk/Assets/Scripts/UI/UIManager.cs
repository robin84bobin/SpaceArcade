using System;
using System.Collections.Generic;
using Assets.Scripts.UI.Windows;
using UnityEngine;

/// <summary>
/// Shows windows.
/// </summary>
public class UIManager : MonoBehaviour 
{
    private Canvas _uiRoot;

    private List<BaseWindow> _shownWindows;
    public void Init(Canvas root)
    {
        _uiRoot = root;
        GameObject.DontDestroyOnLoad(_uiRoot.gameObject);
        _shownWindows = new List<BaseWindow>();
    }

    public BaseWindow ShowWindow(string windowName_, WindowParams param_ = null)
    {
        HideAllWindows();
        GameObject windowGo = LoadPrefab(windowName_);
        BaseWindow newWindow = InstantiateWindow(windowGo);
        newWindow.OnShowComplete(param_);
        _shownWindows.Add(newWindow);

        return newWindow;
    }

    GameObject LoadPrefab(string prefabName_)
    {
        string path = string.Format ("UI/Windows/{0}", prefabName_);
        GameObject go = Resources.Load(path) as GameObject;
        go.name = prefabName_;
        return go;
    }

    
    BaseWindow InstantiateWindow(GameObject windowGo_)
    {
        GameObject parent = _uiRoot.gameObject;
        GameObject go = GameObject.Instantiate(windowGo_) as GameObject;
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent.transform, false);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
            go.SetActive(true);
        }

        return go.GetComponent<BaseWindow> ();
    }
    

    public void HideAllWindows ()
    {
        for (int i = 0; i < _shownWindows.Count; i++) {
            _shownWindows[i].Hide();
        }
    }

    public void HideWindow (BaseWindow window_)
    {
        _shownWindows.Remove(window_);

        window_.transform.parent = null;
        GameObject.Destroy(window_.gameObject);
    }
}

