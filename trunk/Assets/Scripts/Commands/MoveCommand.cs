using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform _transform;
    private Vector3 _newPos;
    private Vector3 _oldPos;

    public MoveCommand(Transform t, Vector3 newPos)
    {
        _transform = t;
        _newPos = newPos;
        _oldPos = _transform.position;
        //Debug.Log(string.Format("MoveCommand: {0} => {1}", _oldPos.ToString(), _newPos.ToString()));
    }

    public override void Execute()
    {
        if (_transform == null) return;
        _transform.position = _newPos;
        //Debug.Log(string.Format("MoveCommand Execute: {0} => {1}", _transform.position.ToString(), _newPos.ToString()));
    }

    public override void Undo()
    {
        if (_transform == null) return;
        _transform.position = _oldPos;
        //Debug.Log(string.Format("MoveCommand Undo: {0} => {1}", _transform.position.ToString(), _oldPos.ToString()));
    }


}

