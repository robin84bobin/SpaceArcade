using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ActorController))]

public class ActorInputController : MonoBehaviour {

    ActorController _actor;
    private bool _enableInput = true;

    void Start () {
        Init();
	}
	
    void Init()
    {
        _actor = GetComponent<ActorController>();
        if (_actor == null)
        {
            Debug.LogError(string.Format("{0} needs {1}", this.GetType().Name, _actor.GetType().Name));
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enableInput) return;

        if (Input.GetKey(KeyCode.Space))
        {
            Attack();//CommandManager.Execute(new AttackCommand(this));
        }
    }

    internal void Attack()
    {
        _actor.actorModel.Attack();
    }

    void Register(bool register)
    {
        if (register)
        InputManager.instance.AddActor(this);
        else
        InputManager.instance.RemoveActor(this);
    }

    void OnDeath()
    {
        _enableInput = false;
    }

    void OnRespawn()
    {
        _enableInput = true;
    }
}
