using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Detects if touch hits panel collider area
/// </summary>
public class TouchController : MonoBehaviour
{
    public static event Action onInit = delegate { };
    private static TouchController _instance;
    public static TouchController Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
            onInit.Invoke();
        }
    }

    public bool touched { get; private set; }
    private Touch _currentTouch { get { return Input.GetTouch(_currTouchId); } }
    public Vector3 touchPosition
    {
        get
        {
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Handheld:
                    Vector3 pos = new Vector3(_currentTouch.position.x, _currentTouch.position.y, 0f);
                    return pos;
                default:
                    return Input.mousePosition;
            }
        }
    }

    public int CurrTouchId
    {
        get
        {
            return _currTouchId;
        }
    }

    private BoxCollider2D _collider2D;
 //   private SwipeMove _swipeMove;

    private delegate bool CheckTouch();
    private CheckTouch _checkTouch;

 /*   public void Init(SwipeMove swipeMove)
    {
        _swipeMove = swipeMove;

    }*/

    void Start ()
    {
        Instance = this;

        _collider2D = GetComponent<BoxCollider2D>();

        switch(SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                _checkTouch = CheckHandheldTouch;
                break;
            case DeviceType.Desktop:
                _checkTouch = CheckDesktopTouch;
                break;
            default:
                _checkTouch = CheckDesktopTouch;
                break;
        }
    }

    
    void Update()
    {
        touched = _checkTouch();
    }

    private bool CheckHandheldTouch()
    {
        GetCurrentTouchId();
        return _currTouchId >= 0;
    }

    int _currTouchId = -1;
    

    void GetCurrentTouchId()
    {
        if (Input.touchCount <= 0)
        {
             _currTouchId = -1;
            return;
        }

        if (_currTouchId < 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (CheckPosition(touch.position))
                {
                    _currTouchId = touch.fingerId;
                    return;
                }
            }
        }
        else
        {
            switch (Input.GetTouch(_currTouchId).phase)
            {
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _currTouchId = -1;
                    break;
            }
        }

    }

    private bool CheckDesktopTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 wp = Input.mousePosition;
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            return CheckPosition(touchPos);
        }
        return false;
    }

    bool CheckPosition(Vector2 pos)
    {
        return _collider2D == Physics2D.OverlapPoint(pos);
    }
    
    void OnGUI()
    {
        return;
        GUILayout.Label("_____PANEL______");
        GUILayout.Label(SystemInfo.deviceType.ToString());
        GUILayout.Label("_currentTouch.fingerId :" + _currentTouch.fingerId.ToString());
        GUILayout.Label("_currTouchId: " + _currTouchId.ToString());
        GUILayout.Label("touchPosition: "  + touchPosition.ToString());
    }

}
