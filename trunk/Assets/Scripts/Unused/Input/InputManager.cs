using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        if (_instance != null)
        {
            Debug.LogError(string.Format("{0}: instance already exist!", this.GetType().Name));
            return;
        }
        _instance = this;
    }


	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Space))
        {
            foreach (var item in _actorsInput)
            {
                item.Attack();
            }
        }
	}

    List<ActorInputController> _actorsInput = new List<ActorInputController>();
    public List<ActorInputController> actorsInput { get { return _actorsInput; } }

    public void AddActor(ActorInputController actorInput)
    {
        if (_actorsInput.Contains(actorInput))
        {
            Debug.LogWarning(string.Format("{0}: actor input controller already registered", this.GetType().Name));
            return;
        }
        _actorsInput.Add(actorInput);
    }

    public void RemoveActor(ActorInputController actorInput)
    {
        if (!_actorsInput.Contains(actorInput))
        {
            Debug.LogWarning(string.Format("{0}: can't remove actor input controller. not registered", this.GetType().Name));
            return;
        }
        _actorsInput.Remove(actorInput);
    }
}
