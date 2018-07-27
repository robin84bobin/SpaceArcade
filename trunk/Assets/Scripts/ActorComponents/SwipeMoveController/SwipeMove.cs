using System;
using Assets.Scripts.ActorControllers.SwipeMoveController;
using UnityEngine;


public enum SwipeRestriction
{
    FREE,
    VERTICAL,
    HORIZONTAL
}

public enum SwipeState
{
    IDLE,
    TOUCHED,
    UNTOUCHED
}

/// <summary>
/// 
/// </summary>
public class SwipeMove : MonoBehaviour
{
    public SwipeRestriction mode = SwipeRestriction.FREE;

    [Range(0,1)]
    public float straightness = 1f;
    [Range(0, 1)]
    public float swipeThreshold = 0.1f;
    [Range(0, 1)]
    public float moveThreshold = 0.05f;

    public FSM<SwipeState, SwipeAbstractState> State { get; private set; }
    private SwipeIdleState _idleState;
    private SwipeTouchedState _touchedState;
    private SwipeUntouchedState _untouchedState;

    private TouchController _area;

    public Vector3 touchPosition
    {
        get
        {
            if (_area != null)
            {
                return _area.touchPosition;
            }
            else
            {
                return Input.mousePosition;
            }
        }
    }

    public bool touched { get
        {
            if (_area != null)
            {
                return _area.touched;
            }
            else
            {
                return Input.GetMouseButton(0);
            }
        }
    }

    void Awake()
    {
        InitTouchArea();
        InitBounds();
        InitStates();
    }

    private void InitTouchArea()
    {
        if (TouchController.Instance != null)
        {
            SetTouchArea();
        }
        else
        {
            TouchController.onInit += SetTouchArea;
        }
    }

    private void SetTouchArea()
    {
        TouchController.onInit -= SetTouchArea;
        _area = TouchController.Instance;
    }

    private Rect _bounds;
    private void InitBounds()
    {
        _bounds = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    public bool CheckBounds(Vector2 pos)
    {
        return _bounds.Contains(pos);
    }

    private void InitStates()
    {
        State = new FSM<SwipeState, SwipeAbstractState>();

        _idleState = new SwipeIdleState(this);

        _touchedState = new SwipeTouchedState(this);
        _touchedState.moveThreshold = moveThreshold;
        _touchedState.swipeThreshold = swipeThreshold;
        _touchedState.straightness = straightness;

        _untouchedState = new SwipeUntouchedState(this);
        _untouchedState.moveThreshold = moveThreshold;
        _untouchedState.straightness = straightness;

        State.Add(SwipeState.IDLE, _idleState);
        State.Add(SwipeState.TOUCHED, _touchedState);
        State.Add(SwipeState.UNTOUCHED, _untouchedState);
    }

    void Start () {
        State.SetState(SwipeState.IDLE);
    }
	
	void Update () {
        State.CurrentState.Update();
	}


    void OnGUI()
    {
        /*
        GUILayout.Label("_____SWIPE______");
        GUILayout.Label(State.CurrentState.ToString());
        GUILayout.Label("CurrTouchId: " + _area.CurrTouchId.ToString());
        GUILayout.Label("touched: " + _area.touched.ToString());
        */
    }
}
