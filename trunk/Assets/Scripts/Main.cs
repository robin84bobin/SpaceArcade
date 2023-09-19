using Assets.Scripts.UI.Windows;
using System;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(DropManager))]
[RequireComponent(typeof(UIManager))]

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; }
    public EventManager Events { get; private set; }
    public UIManager UI { get; private set; }
    public DropManager Drop { get; private set; }

    private DataController _data;
    public DataController Data
    {
        get
        {
            return _data;
        }
    }

    [NonSerialized]
    public LevelController level;
    
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(string.Format("{0}: instance already exist!", this.GetType().Name));
            return;
        }
        Instance = this;
        Init();
    }

    void Init()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        DontDestroyOnLoad(gameObject);

        //Managers initialisation
        Events = gameObject.GetComponent<EventManager>();
        Drop = gameObject.GetComponent<DropManager>();
        UI = gameObject.GetComponent<UIManager>();
        Canvas uiRoot = FindObjectOfType<Canvas>();
        UI.Init(uiRoot);

        //Show main menu on start game
        MenuWindow.Show();
    }

    /// <summary>
    /// Loads scene and shows preloader while scene loading
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        UI.HideAllWindows();
        Preloader preloader = Preloader.Show(sceneName);
    }

}

